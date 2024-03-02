using API.Contracts;

namespace API.Services {
    public interface ICoachService
    {
        IEnumerable<CoachModel> GetAllCoaches();
        OperationResult<CoachModel> GetCoachById(Guid id);
        OperationResult<Guid> CreateCoach(CoachModel user);
        OperationResult<string> UpdateCoach(Guid id, CoachModel user);
        public OperationResult<string> DeleteCoach(Guid id);
    }
}
