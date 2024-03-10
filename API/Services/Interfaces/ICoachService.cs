using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface ICoachService //: IBaseService<CoachModel>
{
    IEnumerable<CoachModel> GetAllCoaches();
    OperationResult<CoachModel> GetCoachById(Guid id);
    OperationResult<Guid> CreateCoach(CoachModel user);
    OperationResult<string> UpdateCoach(Guid id, CoachModel user);
    OperationResult<string> DeleteCoach(Guid id);
}