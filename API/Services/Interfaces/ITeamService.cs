using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface ITeamService
{
    IEnumerable<TeamReadModel> GetAllTeams();
    OperationResult<TeamReadModel> GetTeamById(Guid id);
    OperationResult<Guid> CreateTeam(TeamWriteModel teamWrite);
    OperationResult<string> UpdateTeam(Guid id, TeamWriteModel teamWrite);
    OperationResult<string> DeleteTeam(Guid id);
}