using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface ICompetitionService
{
    IEnumerable<CompetitionReadModel> GetAllCompetitions();
    IEnumerable<CompetitionReadModel> GetAllUserCompetitions(Guid userId);
    OperationResult<CompetitionReadModel> GetCompetitionById(Guid id);
    Task<OperationResult<Guid>> CreateCompetition(CompetitionWriteModel competition);
    Task<OperationResult<string>> UpdateCompetition(Guid id, CompetitionReadModel user);
    Task<OperationResult<string>> DeleteCompetition(Guid id);
    Task<OperationResult<string>> AddUserToCompetition(Guid competitionId, IEnumerable<Guid> userId);
    
}