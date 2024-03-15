using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface IRoutineService
{
    IEnumerable<RoutineReadModel> GetAllRoutines();
    OperationResult<RoutineReadModel> GetRoutineById(Guid id);
    OperationResult<Guid> CreateRoutine(RoutineWriteModel routine);
    OperationResult<string> UpdateRoutine(Guid id, RoutineWriteModel routine);
    OperationResult<string> DeleteRoutine(Guid id);
}