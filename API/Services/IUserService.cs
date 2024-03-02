using API.Contracts;

namespace API.Services {
    public interface IUserService
    {
        IEnumerable<UserReadModel> GetAllUsers();
        OperationResult<UserReadModel> GetUserById(Guid id);
        OperationResult<Guid> CreateUser(UserWriteModel user);
        OperationResult<string> UpdateUser(Guid id, UserWriteModel user);
        public OperationResult<string> DeleteUser(Guid id);
    }
}
