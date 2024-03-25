using API.Contracts;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserRoleController : ControllerBase
{
    private readonly IUserRoleService _userRoleService;

    public UserRoleController(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    [HttpGet("/userRoles/{userid}")]
    public ActionResult<IEnumerable<UserRoleModel>> Get(Guid userId)
    {
        var userRoles = _userRoleService.GetUserRoles(userId);
        return Ok(userRoles);
    }

    [HttpPost("/user/{userId}/role/{roleId}")]
    public IActionResult AddUserRole(Guid userId, int roleId)
    {
        var result = _userRoleService.AddUserRole(userId, roleId);

        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpDelete("{userId}/{roleId}")]
    public IActionResult DeleteUserRole(Guid userId, int roleId)
    {
        var result = _userRoleService.DeleteUserRole(userId, roleId);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }
}