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

    public IEnumerable<TeamModel> GetAllTeams()
    {
        var teams = _mapper.Map<IEnumerable<TeamModel>>(_dbContext.Teams);
        return teams;
    }

    public OperationResult<TeamModel> GetTeamById(Guid id)
    {
        var team = _dbContext.Teams.FirstOrDefault(x => x.Id == id);
        if (team == null) return OperationResult<TeamModel>.Fail($"Team with id {id} does not exist");

        var teamModel = _mapper.Map<TeamModel>(team);
        return OperationResult<TeamModel>.Success(teamModel);
    }

    public OperationResult<Guid> CreateTeam(TeamModel team)
    {
        var existingTeamName = _dbContext.Teams.FirstOrDefault(x => x.Name == team.Name && x.Location == team.Location);
        if (existingTeamName != null)
            return OperationResult<Guid>.Fail("Team with this name and location already exists");

        var newTeam = _mapper.Map<Team>(team);
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

    public OperationResult<string> UpdateTeam(Guid id, TeamModel team)
    {
        var existingTeam = _dbContext.Teams.FirstOrDefault(x => x.Id == id);
        if (existingTeam == null) return OperationResult<string>.Fail("Team does not exist");

        var existingTeamName = _dbContext.Teams.FirstOrDefault(x => x.Name == team.Name && x.Location == team.Location);
        if (existingTeamName != null)
            return OperationResult<string>.Fail("Team with this name and location already exists");

        _mapper.Map(team, existingTeam);

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