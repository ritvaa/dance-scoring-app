using API.Contracts;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TeamReadModel>> Get()
    {
        var teams = _teamService.GetAllTeams();
        return Ok(teams);
    }

    [HttpGet("{id}")]
    public ActionResult<TeamReadModel> GetById(Guid id)
    {
        var result = _teamService.GetTeamById(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPost]
    public IActionResult CreateTeam(TeamWriteModel teamWrite)
    {
        var result = _teamService.CreateTeam(teamWrite);

        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPut("{id}")]
    public ActionResult<TeamWriteModel> Update(Guid id, TeamWriteModel updatedTeamWrite)
    {
        var result = _teamService.UpdateTeam(id, updatedTeamWrite);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTeam(Guid id)
    {
        var result = _teamService.DeleteTeam(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }
}