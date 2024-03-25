using API.Contracts;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoutineController : ControllerBase
{
    private readonly IRoutineService _routineService;

    public RoutineController(IRoutineService routineService)
    {
        _routineService = routineService;
    }

    [HttpGet("{competitionId}")]
    public ActionResult<IEnumerable<RoutineReadModel>> Get(Guid competitionId)
    {
        var routines = _routineService.GetAllRoutines(competitionId);
        return Ok(routines);
    }

    [HttpGet("{id}")]
    public ActionResult<RoutineReadModel> GetById(Guid id)
    {
        var result = _routineService.GetRoutineById(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoutine(RoutineWriteModel routine)
    {
        var result = await _routineService.CreateRoutine(routine);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPut("{id}")]
    public ActionResult<RoutineReadModel> Update(Guid id, RoutineWriteModel routine)
    {
        var result = _routineService.UpdateRoutine(id, routine);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRoutine(Guid id)
    {
        var result = _routineService.DeleteRoutine(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }
    
    [HttpGet("/byCategory/{competitionId}")]
    public ActionResult<RoutineReadModel> GetRoutinesByCategory(Guid competitionId)
    {
        var routines = _routineService.GetCompetitionRoutinesByCategory(competitionId);
        return Ok(routines);
    }
    
    [HttpGet("{competitionId}/category/{categoryId}")]
    public ActionResult<RoutineReadModel> GetCategoryRoutines(Guid competitionId, int categoryId)
    {
        var routines = _routineService.GetCategoryRoutines(competitionId, categoryId);
        return Ok(routines);
    }
}