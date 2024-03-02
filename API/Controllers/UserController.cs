using API.Contracts;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<UserReadModel>> Get()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult<UserReadModel> GetById(Guid id)
    {
        var result = _userService.GetUserById(id);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return Conflict(result.ErrorMessage);
    }
    
    [HttpPost]
    public IActionResult CreateUser(UserWriteModel user)
    {
        var result = _userService.CreateUser(user);

        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return Conflict(result.ErrorMessage);
    }
    
    [HttpPut("{id}")]
    public ActionResult<UserReadModel> Update(Guid id, UserWriteModel updatedUser)
    {
        var result = _userService.UpdateUser(id, updatedUser);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return Conflict(result.ErrorMessage);
    }

    [HttpPost]
    public IActionResult DeleteUser(Guid id)
    {
        var result = _userService.DeleteUser(id);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return Conflict(result.ErrorMessage);


    }
}