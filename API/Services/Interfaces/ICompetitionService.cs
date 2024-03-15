using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface ICompetitionService
{
    IEnumerable<CompetitionReadModel> GetAllCompetitions();
    OperationResult<CompetitionReadModel> GetCompetitionById(Guid id);
    OperationResult<Guid> CreateCompetition(CompetitionReadModel user);
    OperationResult<string> UpdateCompetition(Guid id, CompetitionReadModel user);
    OperationResult<string> DeleteCompetition(Guid id);
    OperationResult<string> AddUsersToCompetition(Guid competitionId, List<Guid> userIds);
}