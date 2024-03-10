using API.Contracts;
using API.Contracts.Results;
using API.Entities;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class CompetitionService : ICompetitionService
{
    private readonly DancerScoringAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public CompetitionService(DancerScoringAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<CompetitionReadModel> GetAllCompetitions()
    {
        var competitions = _mapper.Map<IEnumerable<CompetitionReadModel>>(_dbContext.Competitions);
        return competitions;
    }

    public OperationResult<CompetitionReadModel> GetCompetitionById(Guid id)
    {
        var competition = _dbContext.Competitions.FirstOrDefault(x => x.Id == id);
        if (competition == null)
            return OperationResult<CompetitionReadModel>.Fail($"Competition with id {id} does not exist");

        var competitionReadModel = _mapper.Map<CompetitionReadModel>(competition);
        return OperationResult<CompetitionReadModel>.Success(competitionReadModel);
    }

    public OperationResult<Guid> CreateCompetition(CompetitionReadModel competition)
    {
        var existingCompetitionName =
            _dbContext.Competitions.FirstOrDefault(x =>
                x.Name == competition.Name && x.StartDate == competition.StartDate);

        if (existingCompetitionName != null)
            return OperationResult<Guid>.Fail("Competition with the same name and date already exists");

        var newCompetition = _mapper.Map<Competition>(competition);
        newCompetition.Id = Guid.NewGuid();

        try
        {
            _dbContext.Competitions.Add(newCompetition);
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            return OperationResult<Guid>.Fail("An error occurred while adding coach: " + ex.Message);
        }

        return OperationResult<Guid>.Success(newCompetition.Id);
    }

    public OperationResult<string> UpdateCompetition(Guid id, CompetitionReadModel competition)
    {
        var existingCompetition = _dbContext.Competitions.FirstOrDefault(x => x.Id == id);
        if (existingCompetition == null) return OperationResult<string>.Fail("Competition does not exist");

        var existingNameAndDate =
            _dbContext.Competitions.FirstOrDefault(x =>
                x.Name == competition.Name && x.StartDate == competition.StartDate);
        if (existingNameAndDate != null)
            return OperationResult<string>.Fail("Competition with this name and date already exists");

        _mapper.Map(competition, existingCompetition);

        _dbContext.SaveChanges();

        return OperationResult<string>.Success($"Competition with id: {id} updated successfully");
    }

    public OperationResult<string> DeleteCompetition(Guid id)
    {
        var existingCompetition = _dbContext.Competitions.FirstOrDefault(x => x.Id == id);
        if (existingCompetition == null) return OperationResult<string>.Fail("Competition not found");

        if (existingCompetition.Users.Count != 0)
            return OperationResult<string>.Fail("Users are already assigned to this competition");

        if (existingCompetition.Routinines.Count != 0)
            return OperationResult<string>.Fail("Routines are already assigned to this competition");

        try
        {
            _dbContext.Competitions.Remove(existingCompetition);
            _dbContext.SaveChanges();
            return OperationResult<string>.Success($"Competition with id: {id} deleted successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Fail($"An error occurred while deleting dancer: {ex.Message}");
        }
    }
}