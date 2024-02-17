using DancerScoringApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DanceScoringApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly DancerScoringAppDbContext _appDbContext;

    public UserController(DancerScoringAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetTodos()
    {
        return _appDbContext.Users.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetTodoById(int id)
    {
        var todo = _appDbContext.Users.Find(id);
        if (todo == null) return NotFound();

        return todo;
    }


    [HttpPost]
    public ActionResult<User> CreateTodo([FromBody] User user)
    {
        _appDbContext.Users.Add(user);
        _appDbContext.SaveChanges();
        return CreatedAtAction(nameof(GetTodoById), new { id = user.Id }, user);
    }
}