using API.Contracts;

namespace API.Services;

public interface IDancerService
{
    IEnumerable<DancerReadModel> GetAllDancers();
    OperationResult<DancerReadModel> GetDancerById(Guid id);
    OperationResult<Guid> CreateDancer(DancerReadModel dancerRead);
    OperationResult<string> UpdateDancer(Guid id, DancerReadModel updatedDancerRead);
    OperationResult<string> DeleteDancer (Guid id);
    OperationResult<IEnumerable<DancerReadModel>> ImportDancersFromCsv(Stream excelStream);
}