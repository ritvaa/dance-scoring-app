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
    public ActionResult<IEnumerable<TeamModel>> Get()
    {
        var teams = _teamService.GetAllTeams();
        return Ok(teams);
    }

    [HttpGet("{id}")]
    public ActionResult<TeamModel> GetById(Guid id)
    {
        var result = _teamService.GetTeamById(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPost]
    public IActionResult CreateTeam(TeamModel team)
    {
        var result = _teamService.CreateTeam(team);

        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPut("{id}")]
    public ActionResult<TeamModel> Update(Guid id, TeamModel updatedTeam)
    {
        var result = _teamService.UpdateTeam(id, updatedTeam);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPost]
    public IActionResult DeleteTeam(Guid id)
    {
        var result = _teamService.DeleteTeam(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }
}