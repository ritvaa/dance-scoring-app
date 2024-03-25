using API.Contracts;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompetitionController : ControllerBase
{
    private readonly ICompetitionService _competitionService;

    public CompetitionController(ICompetitionService competitionService)
    {
        _competitionService = competitionService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CompetitionReadModel>> Get()
    {
        var competitions = _competitionService.GetAllCompetitions();
        return Ok(competitions);
    }

    [HttpGet("{id}")]
    public ActionResult<CompetitionReadModel> GetById(Guid id)
    {
        var result = _competitionService.GetCompetitionById(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompetition([FromBody] CompetitionWriteModel competition)
    {
        var result = await _competitionService.CreateCompetition(competition);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CompetitionReadModel>> Update(Guid id, CompetitionReadModel updatedCompetitionRead)
    {
        var result = await _competitionService.UpdateCompetition(id, updatedCompetitionRead);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompetition(Guid id)
    {
        var result = await _competitionService.DeleteCompetition(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }
    
    [HttpPost("/addUsersToCompetition/{competitionId}")]
    public async Task<IActionResult> AddUserToCompetition(Guid competitionId, [FromBody] IEnumerable<Guid> userIds)
    {
        var result = await _competitionService.AddUserToCompetition(competitionId, userIds);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }
    
    [HttpGet("/getUserCompetitions")]
    public ActionResult<IEnumerable<CompetitionReadModel>> Get(Guid userId)
    {
        var competitions = _competitionService.GetAllUserCompetitions(userId);
        return Ok(competitions);
    }
    
}