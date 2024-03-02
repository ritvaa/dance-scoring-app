using API.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class BonusController : ControllerBase
{
    private readonly DancerScoringAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public BonusController(DancerScoringAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    [HttpGet]
    public IEnumerable<BonusModel> Get()
    {
        var result = _mapper.Map<IEnumerable<BonusModel>>(_dbContext.Bonuses);
        return result;
    }
    
    [HttpGet("{id}")]
    public BonusModel GetById(int id)
    {
        var result = _mapper.Map<BonusModel>(_dbContext.Bonuses.FirstOrDefault(x=>x.Id == id));
        return result;
    }
}