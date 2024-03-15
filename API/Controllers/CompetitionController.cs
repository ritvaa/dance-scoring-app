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
    public IActionResult CreateCompetition(CompetitionReadModel competitionRead)
    {
        var result = _competitionService.CreateCompetition(competitionRead);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPut("{id}")]
    public ActionResult<CompetitionReadModel> Update(Guid id, CompetitionReadModel updatedCompetitionRead)
    {
        var result = _competitionService.UpdateCompetition(id, updatedCompetitionRead);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCompetition(Guid id)
    {
        var result = _competitionService.DeleteCompetition(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }
    
    //todo send email to dancers after finished competition
    
    [HttpPost("competition/{competitionId}/users/{userIds}")] //wtf is this 
    public IActionResult AddUsersToCompetition(Guid competitionId, List<Guid> userIds)
    {
        var result = _competitionService.AddUsersToCompetition(competitionId, userIds);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }
}