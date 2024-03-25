using API.Contracts;
using API.Contracts.Results;
using API.Entities;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class SquadService : ISquadService
{
    private readonly DancerScoringAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public SquadService(DancerScoringAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public IEnumerable<SquadReadModel> GetSquads(Guid teamId)
    {
        var squads = _dbContext.Squads
            .Include(x=>x.Team)
            .Include(x=>x.Dancers)
            .ThenInclude(x=>x.Dancer)
            .Where(x => x.Team.Id == teamId);
        return _mapper.Map<IEnumerable<SquadReadModel>>(squads);
    }

    public OperationResult<SquadReadModel> GetSquadById(Guid teamId, Guid id)
    {
        var squad = _dbContext.Squads.FirstOrDefault(x => x.Id == id);
        if (squad == null) return OperationResult<SquadReadModel>.Fail($"Squad with id {id} does not exist");

        var squadModel = _mapper.Map<SquadReadModel>(squad);
        return OperationResult<SquadReadModel>.Success(squadModel);
    }

    public OperationResult<Guid> CreateSquad(SquadWriteModel squad)
    {
        var newSquad = _mapper.Map<Squad>(squad);
        newSquad.Id = Guid.NewGuid();

        try
        {
            _dbContext.Squads.Add(newSquad);
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            return OperationResult<Guid>.Fail("An error occurred while adding squad: " + ex.Message);
        }

        return OperationResult<Guid>.Success(newSquad.Id);
    }

    public OperationResult<string> UpdateSquad(Guid id, SquadWriteModel squad)
    {
        var existingSquad = _dbContext.Squads.FirstOrDefault(x => x.Id == id);
        if (existingSquad == null) return OperationResult<string>.Fail("Squad does not exist");

        _mapper.Map(squad, existingSquad);
        _dbContext.SaveChanges();

        return OperationResult<string>.Success($"Squad with id: {id} updated successfully");
    }

    public OperationResult<string> DeleteSquad(Guid id)
    {
        var existingSquad = _dbContext.Squads.FirstOrDefault(x => x.Id == id);
        if (existingSquad == null) return OperationResult<string>.Fail("Squad not found");
        try
        {
            _dbContext.Squads.Remove(existingSquad);
            _dbContext.SaveChanges();
            return OperationResult<string>.Success($"Squad with id: {id} deleted successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Fail($"An error occurred while deleting squad: {ex.Message}");
        }
    }

    public async Task<OperationResult<string>> AddDancerToSquad(Guid squadId, List<Guid> dancerIds)
    {
        var squad = await _dbContext.Squads.FirstOrDefaultAsync(x => x.Id == squadId);
        if(squad == null) return OperationResult<string>.Fail("Squad not found");

        switch (squad.SquadType)
        {
            case SquadType.Formation:
                if (dancerIds.Count < 8 || dancerIds.Count > 25)
                {
                    return OperationResult<string>.Fail("Formation can only have 8 to 25 dancers");
                }

                break;
            case SquadType.MiniFormation:
                if (dancerIds.Count < 4 || dancerIds.Count > 7)
                {
                    return OperationResult<string>.Fail("Mini Formation can only have 4 to 7 dancers");
                }

                break;
            case SquadType.DuoTrio:
                if (dancerIds.Count < 2 || dancerIds.Count > 3)
                {
                    return OperationResult<string>.Fail("Duo/trio category can only have 2 or 3 dancers");
                }

                break;
            case SquadType.Solo:
                if (dancerIds.Count > 1)
                {
                    return OperationResult<string>.Fail("Solo can only have 1 dancer");
                }

                break;
        }

        foreach (var dancerId in dancerIds)
        {
            try
            {
                await _dbContext.AddAsync(new SquadDancer
                {
                    Id = Guid.NewGuid(),
                    DancerId = dancerId,
                    SquadId = squad.Id,
                });
                await _dbContext.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                return OperationResult<string>.Fail($"An error occurred while adding dancers to squad: {ex.Message}");
            }
        }
        return OperationResult<string>.Success($"Dancers are added to squad");
    }
}