using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface IPenaltyPointsService
{
    IEnumerable<PenaltyPointsReadModel> GetAllPenaltyPoints();
    IEnumerable<PenaltyPointsReadModel> GetPenaltyPointsByScore(decimal score);
    OperationResult<PenaltyPointsReadModel> GetPenaltyPointById(int id);
    
}