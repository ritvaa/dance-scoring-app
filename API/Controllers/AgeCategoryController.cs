using API.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AgeCategoriesController : ControllerBase
{
    private readonly DancerScoringAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public AgeCategoriesController(DancerScoringAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    [HttpGet]
    public IEnumerable<AgeCategoryModel> Get()
    {
        var result = _mapper.Map<IEnumerable<AgeCategoryModel>>(_dbContext.AgeCategories);
        return result;
    }
    
    [HttpGet("{id}")]
    public AgeCategoryModel GetById(int id)
    {
        var result = _mapper.Map<AgeCategoryModel>(_dbContext.AgeCategories.FirstOrDefault(x=>x.Id == id));
        return result;
    }
}