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
    
    [HttpPost("techJudgeRating/{judgeId}/routine/{routineId}")]
    public IActionResult AddTechJudgeRating(Guid routineId, Guid judgeId, [FromBody] TechJudgeRatingWriteModel judgeRating)
    {
        var ratings = _ratingService.AddTechJudgeRatingToRoutine(routineId, judgeId, judgeRating);
        return Ok(ratings);
    }

    // [HttpGet("{id}")]
    // public ActionResult<RatingReadModel> GetById(Guid id)
    // {
    //     var result = _ratingService.GetRatingById(id);
    //     if (result.IsSuccess) return Ok(result.Data);
    //     return Conflict(result.ErrorMessage);
    // }
    //
    // [HttpPost]
    // public IActionResult CreateRating(RatingWriteModel rating)
    // {
    //     var result = _ratingService.CreateRating(rating);
    //     if (result.IsSuccess) return Ok(result.Data);
    //     return Conflict(result.ErrorMessage);
    // }
    //
    // [HttpPut("{id}")]
    // public ActionResult<RatingReadModel> Update(Guid id, RatingWriteModel updatedRating)
    // {
    //     var result = _ratingService.UpdateRating(id, updatedRating);
    //     if (result.IsSuccess) return Ok(result.Data);
    //     return Conflict(result.ErrorMessage);
    // }
    //
    // [HttpDelete("{id}")]
    // public IActionResult DeleteRating(Guid id)
    // {
    //     var result = _ratingService.DeleteRating(id);
    //     if (result.IsSuccess) return Ok(result.Data);
    //     return Conflict(result.ErrorMessage);
    // }
}