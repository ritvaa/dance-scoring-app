using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface IUserService
{
    IEnumerable<UserReadModel> GetAllUsers();
    OperationResult<UserReadModel> GetUserById(Guid id);
    OperationResult<Guid> CreateUser(UserWriteModel user);
    OperationResult<string> UpdateUser(Guid id, UserWriteModel user);
    OperationResult<string> DeleteUser(Guid id);
}