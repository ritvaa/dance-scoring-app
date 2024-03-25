using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface ICategoryService
{
    IReadOnlyCollection<CategoryReadModel> GetAllCategories();
    OperationResult<CategoryReadModel> GetCategoryById(int id);
}