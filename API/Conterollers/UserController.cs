using API.Contracts;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Conterollers;

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
    public ActionResult<IEnumerable<User>> Get()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetById(Guid id)
    {
        var user = _userService.GetUserById(id);

        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> Create(UserWriteModel user)
    {
        var createdUser = _userService.CreateUser(user);
        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
    }
    
    [HttpPut("{id}")]
    public ActionResult<UserReadModel> Update(Guid id, UserWriteModel updatedUser)
    {
        try
        {
            var result = _userService.UpdateUser(id, updatedUser);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _userService.DeleteUser(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}