using API.Contracts;
using API.Contracts.Results;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly DancerScoringAppDbContext _dbContext;

    public CategoryService(IMapper mapper, DancerScoringAppDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    public IReadOnlyCollection<CategoryReadModel> GetAllCategories()
    {
        
        //var categories = _mapper.Map<IReadOnlyCollection<CategoryReadModel>>(_dbContext.Categories);
        var categories = _dbContext.Categories;
        return _mapper.Map<IReadOnlyCollection<CategoryReadModel>>(categories);
    }

    public OperationResult<CategoryReadModel> GetCategoryById(int id)
    {
        var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
        if (category == null)
            return OperationResult<CategoryReadModel>.Fail($"Category with id {id} does not exist");

        var competitionReadModel = _mapper.Map<CategoryReadModel>(category);
        return OperationResult<CategoryReadModel>.Success(competitionReadModel);
    }
}