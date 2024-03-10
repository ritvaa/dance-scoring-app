using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface IUserRoleService
{
    IEnumerable<UserRoleModel> GetUserRoles(Guid id);
    OperationResult<Guid> AddUserRole(Guid userId, Guid roleId);
    OperationResult<string> DeleteUserRole(Guid userId, Guid roleId);
    
}