using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface ITeamService
{
    IEnumerable<TeamModel> GetAllTeams();
    OperationResult<TeamModel> GetTeamById(Guid id);
    OperationResult<Guid> CreateTeam(TeamModel team);
    OperationResult<string> UpdateTeam(Guid id, TeamModel team);
    OperationResult<string> DeleteTeam(Guid id);
}