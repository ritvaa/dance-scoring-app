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
    public IEnumerable<BonusReadModel> Get()
    {
        var result = _mapper.Map<IEnumerable<BonusReadModel>>(_dbContext.Bonuses);
        return result;
    }

    [HttpGet("{id}")]
    public BonusReadModel GetById(int id)
    {
        var result = _mapper.Map<BonusReadModel>(_dbContext.Bonuses.FirstOrDefault(x => x.Id == id));
        return result;
    }
}