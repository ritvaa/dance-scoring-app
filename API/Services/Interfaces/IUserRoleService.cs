using API.Contracts;
using API.Contracts.Results;

namespace API.Services.Interfaces;

public interface IUserRoleService
{
    IEnumerable<UserRoleModel> GetUserRoles(Guid id);
    OperationResult<string> AddUserRole(Guid userId, int roleId);
    OperationResult<string> DeleteUserRole(Guid userId, int roleId);
    
}