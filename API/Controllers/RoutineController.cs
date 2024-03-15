using API.Contracts;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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

    [HttpGet]
    public ActionResult<IEnumerable<RoutineReadModel>> Get()
    {
        var routines = _routineService.GetAllRoutines();
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
    public IActionResult CreateRoutine(RoutineWriteModel routine)
    {
        var result = _routineService.CreateRoutine(routine);
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
    
    // [HttpPut] todo
    // public ActionResult<RoutineReadModel> SetGrandPrixToBestRoutine()
    // {
    //     var result = _routineService.SetGrandPrix();
    //     if (result.IsSuccess) return Ok(result.Data);
    //     return Conflict(result.ErrorMessage);
    // }
    
}