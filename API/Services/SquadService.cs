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
    public IEnumerable<SquadModel> GetSquads(Guid teamId)
    {
        var squads = _mapper.Map<IEnumerable<SquadModel>>(_dbContext.Squads.Where(x => x.Team.Id == teamId));
        return squads;
    }

    public OperationResult<SquadModel> GetSquadById(Guid teamId, Guid id)
    {
        var squad = _dbContext.Squads.FirstOrDefault(x => x.Id == id);
        if (squad == null) return OperationResult<SquadModel>.Fail($"Squad with id {id} does not exist");

        var squadModel = _mapper.Map<SquadModel>(squad);
        return OperationResult<SquadModel>.Success(squadModel);
    }

    public OperationResult<Guid> CreateSquad(SquadModel squad)
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

    public OperationResult<string> UpdateSquad(Guid id, SquadModel squad)
    {
        var existingSquad = _dbContext.Squads.FirstOrDefault(x => x.Id == id);
        if (existingSquad == null) return OperationResult<string>.Fail("User does not exist");

        _mapper.Map(squad, existingSquad);
        _dbContext.SaveChanges();

        return OperationResult<string>.Success($"User with id: {id} updated successfully");
    }

    public OperationResult<string> DeleteSquad(Guid id)
    {
        var existingSquad = _dbContext.Squads.FirstOrDefault(x => x.Id == id);
        if (existingSquad == null) return OperationResult<string>.Fail("Team not found");
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
}