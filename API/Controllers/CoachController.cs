using API.Contracts;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoachController : ControllerBase
{
    private readonly ICoachService _coachService;

    public CoachController(ICoachService coachService)
    {
        _coachService = coachService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CoachModel>> Get()
    {
        var result = _coachService.GetAllCoaches();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<CoachModel> GetById(Guid id)
    {
        var result = _coachService.GetCoachById(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPost]
    public IActionResult CreateCoach(CoachModel coach)
    {
        var result = _coachService.CreateCoach(coach);

        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPut("{id}")]
    public ActionResult<CoachModel> Update(Guid id, CoachModel updatedCoach)
    {
        var result = _coachService.UpdateCoach(id, updatedCoach);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPost]
    public IActionResult DeleteCoach(Guid id)
    {
        var result = _coachService.DeleteCoach(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }
}