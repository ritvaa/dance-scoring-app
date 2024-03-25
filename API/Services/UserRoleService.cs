using API.Contracts;
using API.Contracts.Results;
using API.Entities;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IMapper _mapper;
    private readonly DancerScoringAppDbContext _dbContext;

    public UserRoleService(IMapper mapper, DancerScoringAppDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    public IEnumerable<UserRoleModel> GetUserRoles(Guid userId)
    {
        var userRoles = _mapper.Map<IEnumerable<UserRoleModel>>(_dbContext.UserRoles.Where(x => x.UserId == userId));
        return userRoles;
    }

    public OperationResult<string> AddUserRole(Guid userId, int roleId)
    {
        var checkIfUserAndRoleExist = CheckIfUserAndRoleExists(userId, roleId);
        if (!checkIfUserAndRoleExist.IsSuccess)
        {
            return OperationResult<string>.Fail(checkIfUserAndRoleExist.ErrorMessage);
        }

        var newUserRole = new UserRole()
        {
            Id = Guid.NewGuid(),
            RoleId = roleId,
            UserId = userId
        };

        try
        {
            _dbContext.UserRoles.Add(newUserRole);
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            return OperationResult<string>.Fail("An error occurred while adding user roles: " + ex.Message);
        }

        return OperationResult<string>.Success("Role was added to user");
    }

    public OperationResult<string> DeleteUserRole(Guid userId, int roleId)
    {
        var checkIfUserAndRoleExist = CheckIfUserAndRoleExists(userId, roleId);
        if (!checkIfUserAndRoleExist.IsSuccess)
        {
            return OperationResult<string>.Fail(checkIfUserAndRoleExist.ErrorMessage);
        }

        var userRole = _dbContext.UserRoles.FirstOrDefault(x => x.UserId == userId && x.RoleId == roleId);
        try
        {
            if (userRole != null) _dbContext.UserRoles.Remove(userRole);
            _dbContext.SaveChanges();
            return OperationResult<string>.Success($"Role {roleId} successfully removed");
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Fail($"An error occurred while deleting user role: {ex.Message}");
        }
    }

    private OperationResult<string> CheckIfUserAndRoleExists(Guid userId, int roleId)
    {
        var existingUser = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
        if (existingUser != null) return OperationResult<string>.Fail("User with this id does not exist");

        var existingRole = _dbContext.Roles.FirstOrDefault(x => x.Id == roleId);
        if (existingRole != null) return OperationResult<string>.Fail("Role with this id does not exist");

        return OperationResult<string>.Success("User and role exist");
    }
}