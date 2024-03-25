using System.Globalization;
using API.Contracts;
using API.Contracts.Results;
using API.Entities;
using API.Services.Interfaces;
using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using DbUpdateException = System.Data.Entity.Infrastructure.DbUpdateException;

namespace API.Services;

public class DancerService : IDancerService
{
    private readonly DancerScoringAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public DancerService(DancerScoringAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<DancerReadModel> GetAllDancers()
    {
        //var dancers = _mapper.Map<IEnumerable<DancerReadModel>>(_dbContext.Dancers.Include(d => d.Team));
        var dancers = _dbContext.Dancers.Include(d => d.Team);


        return _mapper.Map<IEnumerable<DancerReadModel>>(dancers);
    }

    public OperationResult<DancerReadModel> GetDancerById(Guid id)
    {
        var dancer = _dbContext.Dancers.Include(x=>x.Team)
            .FirstOrDefault(x => x.Id == id);
        if (dancer == null) return OperationResult<DancerReadModel>.Fail($"Dancer with id {id} does not exist");

        var dancerModel = _mapper.Map<DancerReadModel>(dancer);
        return OperationResult<DancerReadModel>.Success(dancerModel);
    }

    public OperationResult<Guid> CreateDancer(DancerWriteModel dancer)
    {
        var existingDancer = _dbContext.Dancers.FirstOrDefault(x =>
            x.FirstName == dancer.FirstName
            && x.LastName == dancer.LastName
            && x.LicenceId == dancer.LicenceId);
        if (existingDancer != null)
            return OperationResult<Guid>.Fail("Dancer with this name and licence ID already exists");

        var newDancer = _mapper.Map<Dancer>(dancer);
        newDancer.Id = Guid.NewGuid();

        var existingTeam =
            _dbContext.Teams.FirstOrDefault(x => x.Name == dancer.TeamName && x.Location == dancer.TeamLocationName);
        if (existingTeam != null) newDancer.Team = existingTeam;

        try
        {
            _dbContext.Dancers.Add(newDancer);
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            return OperationResult<Guid>.Fail("An error occurred while adding dancer: " + ex.Message);
        }

        return OperationResult<Guid>.Success(newDancer.Id);
    }

    public OperationResult<string> UpdateDancer(Guid id, DancerWriteModel dancerRead)
    {
        var existingDancer = _dbContext.Dancers.FirstOrDefault(x => x.Id == id);
        if (existingDancer == null) return OperationResult<string>.Fail("Dancer does not exist");

        var existingEmail = _dbContext.Dancers.FirstOrDefault(x => x.Email == dancerRead.Email && x.Id != id);
        if (existingEmail != null) return OperationResult<string>.Fail("Dancer with this email already exists");

        _mapper.Map(dancerRead, existingDancer);

        _dbContext.SaveChanges();

        return OperationResult<string>.Success($"Dancer with id: {id} updated successfully");
    }

    public OperationResult<string> DeleteDancer(Guid id)
    {
        var existingDancer = _dbContext.Dancers.FirstOrDefault(x => x.Id == id);
        if (existingDancer == null) return OperationResult<string>.Fail("Dancer not found");

        try
        {
            _dbContext.Dancers.Remove(existingDancer);
            _dbContext.SaveChanges();
            return OperationResult<string>.Success($"Dancer with id: {id} deleted successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Fail($"An error occurred while deleting dancer: {ex.Message}");
        }
    }

    public OperationResult<IEnumerable<DancerReadModel>> ImportDancersFromCsv(Stream csvStream)
    {
        try
        {
            var dancers = new List<DancerImportModel>();
            var addedDancers = new List<Dancer>();

            using (var reader = new StreamReader(csvStream))
            using (var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                   {
                       HeaderValidated = null,
                       MissingFieldFound = null
                   }))
            {
                var records = csvReader.GetRecords<DancerImportModel>();

                foreach (var record in records) dancers.Add(record);
            }

            foreach (var dancer in dancers) dancer.Id = Guid.NewGuid();

            var dancerEntities = _mapper.Map<List<Dancer>>(dancers);

            foreach (var dancer in dancerEntities)
            {
                if (!string.IsNullOrEmpty(dancer.Team.Name))
                {
                    var team = _dbContext.Teams.FirstOrDefault(t => t.Name == dancer.Team.Name);
                    if (team == null)
                    {
                        team = new Team
                            { Id = Guid.NewGuid(), Name = dancer.Team.Name, Location = dancer.Team.Location };
                        _dbContext.Teams.Add(team);
                        _dbContext.SaveChanges();
                    }

                    dancer.Team = team;
                }

                _dbContext.Dancers.Add(dancer);
                addedDancers.Add(dancer);
            }

            _dbContext.SaveChanges();
            var result = _mapper.Map<IEnumerable<DancerReadModel>>(addedDancers);

            return OperationResult<IEnumerable<DancerReadModel>>.Success(result);
        }
        catch (Exception ex)
        {
            return OperationResult<IEnumerable<DancerReadModel>>.Fail("An error occurred while importing dancers: " +
                                                                      ex.Message);
        }
    }
}