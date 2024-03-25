using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface IDancerService
{
    IEnumerable<DancerReadModel> GetAllDancers();
    OperationResult<DancerReadModel> GetDancerById(Guid id);
    OperationResult<Guid> CreateDancer(DancerWriteModel dancerRead);
    OperationResult<string> UpdateDancer(Guid id, DancerWriteModel updatedDancerRead);
    OperationResult<string> DeleteDancer(Guid id);
    OperationResult<IEnumerable<DancerReadModel>> ImportDancersFromCsv(Stream excelStream);
}