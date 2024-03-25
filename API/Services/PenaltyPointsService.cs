using API.Contracts;
using API.Contracts.Results;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services;

public class PenaltyPointsService : IPenaltyPointsService
{
    private readonly IMapper _mapper;
    private readonly DancerScoringAppDbContext _dbContext;

    public PenaltyPointsService(IMapper mapper, DancerScoringAppDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    public IEnumerable<PenaltyPointsReadModel> GetAllPenaltyPoints()
    {
        var result = _mapper.Map<IEnumerable<PenaltyPointsReadModel>>(_dbContext.PenaltyPoints);
        return result;
    }

    public IEnumerable<PenaltyPointsReadModel> GetPenaltyPointsByScore(decimal score)
    {
        var result = _mapper.Map<IEnumerable<PenaltyPointsReadModel>>(_dbContext.PenaltyPoints.Where(x=>x.PenaltyScore == score));
        return result;
    }

    public OperationResult<PenaltyPointsReadModel> GetPenaltyPointById(int id)
    {
        var penaltyPoint = _dbContext.PenaltyPoints.FirstOrDefault(x => x.Id == id);
        if (penaltyPoint == null)
            return OperationResult<PenaltyPointsReadModel>.Fail($"Penalty point with id {id} does not exist");
        
        var penaltyPointReadModel = _mapper.Map<PenaltyPointsReadModel>(penaltyPoint);
        return OperationResult<PenaltyPointsReadModel>.Success(penaltyPointReadModel);
    }
}