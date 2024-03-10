using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface IDancerService
{
    IEnumerable<DancerReadModel> GetAllDancers();
    OperationResult<DancerReadModel> GetDancerById(Guid id);
    OperationResult<Guid> CreateDancer(DancerReadModel dancerRead);
    OperationResult<string> UpdateDancer(Guid id, DancerReadModel updatedDancerRead);
    OperationResult<string> DeleteDancer(Guid id);
    OperationResult<IEnumerable<DancerReadModel>> ImportDancersFromCsv(Stream excelStream);
}