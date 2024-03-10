using API.Contracts;
using API.Contracts.Results;
using API.Services.Interfaces;

namespace API.Services;

public class UserRoleService : IUserRoleService
{
    public IEnumerable<UserRoleModel> GetUserRoles(Guid id)
    {
        throw new NotImplementedException();
    }

    public OperationResult<Guid> AddUserRole(Guid userId, Guid roleId)
    {
        throw new NotImplementedException();
    }

    public OperationResult<string> DeleteUserRole(Guid userId, Guid roleId)
    {
        throw new NotImplementedException();
    }
}