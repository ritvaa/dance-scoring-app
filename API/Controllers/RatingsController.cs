using API.Contracts;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingController : ControllerBase
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }
    
    [HttpGet("routine/{routineId}")]
    public ActionResult<RatingReadModel> GetRatingForRoutine(Guid routineId)
    {
        var result = _ratingService.GetRatingForRoutine(routineId);
        return Ok(result);
    }
    
    [HttpPost("judgeRating/{judgeId}/routine/{routineId}")]
    public IActionResult AddJudgeRating(Guid routineId, Guid judgeId, [FromBody] JudgeRatingWriteModel judgeRating)
    {
        var ratings = _ratingService.AddJudgeRatingToRoutine(routineId, judgeId, judgeRating);
        return Ok(ratings);
    }
    
    [HttpPut("judgeRating/{judgeId}/routine/{routineId}")]
    public IActionResult UpdateJudgeRating(Guid routineId, Guid judgeId, [FromBody] JudgeRatingWriteModel judgeRating)
    {
        var ratings = _ratingService.UpdateJudgeRatingToRoutine(routineId, judgeId, judgeRating);
        return Ok(ratings);
    }
    
    [HttpPost("techJudgeRating/{judgeId}/routine/{routineId}")]
    public IActionResult AddTechJudgeRating(Guid routineId, Guid judgeId, [FromBody] TechJudgeRatingWriteModel judgeRating)
    {
        var ratings = _ratingService.AddTechJudgeRatingToRoutine(routineId, judgeId, judgeRating);
        return Ok(ratings);
    }
    
    [HttpGet("exportRatings/{competitionId}")]
    public void ExportRoutinesToCsv(Guid competitionId)
    {
        var filePath = "C:\\Code\\dance-scoring-app\\Exports.csv"; 
        _ratingService.ExportRoutineScoresToCsvByCategory(filePath, competitionId);
    }
}