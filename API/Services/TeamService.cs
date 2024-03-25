using API.Contracts;
using API.Contracts.Results;
using API.Entities;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class TeamService : ITeamService
{
    private readonly DancerScoringAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public TeamService(DancerScoringAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<TeamReadModel> GetAllTeams()
    {
        var teams = _mapper.Map<IEnumerable<TeamReadModel>>(_dbContext.Teams);
        return teams;
    }

    public OperationResult<TeamReadModel> GetTeamById(Guid id)
    {
        var team = _dbContext.Teams.FirstOrDefault(x => x.Id == id);
        if (team == null) return OperationResult<TeamReadModel>.Fail($"Team with id {id} does not exist");

        var teamModel = _mapper.Map<TeamReadModel>(team);
        return OperationResult<TeamReadModel>.Success(teamModel);
    }

    public OperationResult<Guid> CreateTeam(TeamWriteModel teamWrite)
    {
        var existingTeamName = _dbContext.Teams.FirstOrDefault(x => x.Name == teamWrite.Name && x.Location == teamWrite.Location);
        if (existingTeamName != null)
            return OperationResult<Guid>.Fail("Team with this name and location already exists");

        var newTeam = _mapper.Map<Team>(teamWrite);
        newTeam.Id = Guid.NewGuid();

        try
        {
            _dbContext.Teams.Add(newTeam);
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            return OperationResult<Guid>.Fail("An error occurred while adding team: " + ex.Message);
        }

        return OperationResult<Guid>.Success(newTeam.Id);
    }

    public OperationResult<string> UpdateTeam(Guid id, TeamWriteModel teamWrite)
    {
        var existingTeam = _dbContext.Teams.FirstOrDefault(x => x.Id == id);
        if (existingTeam == null) return OperationResult<string>.Fail("Team does not exist");

        var existingTeamName = _dbContext.Teams.FirstOrDefault(x => x.Name == teamWrite.Name && x.Location == teamWrite.Location);
        if (existingTeamName != null)
            return OperationResult<string>.Fail("Team with this name and location already exists");

        _mapper.Map(teamWrite, existingTeam);

        _dbContext.SaveChanges();

        return OperationResult<string>.Success($"Team with id: {id} updated successfully");
    }

    public OperationResult<string> DeleteTeam(Guid id)
    {
        var existingTeam = _dbContext.Teams.FirstOrDefault(x => x.Id == id);
        if (existingTeam == null) return OperationResult<string>.Fail("Team not found");

        if (existingTeam.Squads.Count != 0)
            return OperationResult<string>.Fail("Squads are already assigned to this team");

        if (existingTeam.Coaches.Count != 0)
            return OperationResult<string>.Fail("Coaches are already assigned to this team");

        try
        {
            _dbContext.Teams.Remove(existingTeam);
            _dbContext.SaveChanges();
            return OperationResult<string>.Success($"Team with id: {id} deleted successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Fail($"An error occurred while deleting team: {ex.Message}");
        }
    }
}