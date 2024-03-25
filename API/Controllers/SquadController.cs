using API.Contracts;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SquadController : ControllerBase
{
    private readonly ISquadService _squadService;

    public SquadController(ISquadService squadService)
    {
        _squadService = squadService;
    }

    [HttpGet("team/{teamId}")]
    public ActionResult<IEnumerable<SquadReadModel>> Get(Guid teamId)
    {
        var squads = _squadService.GetSquads(teamId);
        return Ok(squads);
    }

    [HttpGet("team/{teamId}/squad/{squadId}")]
    public ActionResult<SquadReadModel> GetById(Guid teamId, Guid squadId)
    {
        var result = _squadService.GetSquadById(teamId, squadId);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPost]
    public IActionResult CreateSquad(SquadWriteModel squad)
    {
        var result = _squadService.CreateSquad(squad);

        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPut("{id}")]
    public ActionResult<SquadWriteModel> Update(Guid id, SquadWriteModel updatedSquad)
    {
        var result = _squadService.UpdateSquad(id, updatedSquad);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSquad(Guid id)
    {
        var result = _squadService.DeleteSquad(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }
    
     [HttpPost("addDancers/{squadId}")]
     public async Task<IActionResult> AddDancerToSquad(Guid squadId, [FromBody] List<Guid> dancerIds)
     {
         var result = await _squadService.AddDancerToSquad(squadId, dancerIds);
         if (result.IsSuccess) return Ok(result.Data);
         return Conflict(result.ErrorMessage);
     }
}