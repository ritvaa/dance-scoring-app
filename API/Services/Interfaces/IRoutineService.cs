using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface IRoutineService
{
    IEnumerable<RoutineReadModel> GetAllRoutines(Guid competitionId);
    OperationResult<RoutineReadModel> GetRoutineById(Guid id);
    Task<OperationResult<Guid>> CreateRoutine(RoutineWriteModel routine);
    OperationResult<string> UpdateRoutine(Guid id, RoutineWriteModel routine);
    OperationResult<string> DeleteRoutine(Guid id);
    IEnumerable<RoutineByCategoryReadModel> GetCompetitionRoutinesByCategory(Guid competitionId);
    IEnumerable<RoutineByCategoryReadModel> GetCategoryRoutines(Guid competitionId, int categoryId);
}