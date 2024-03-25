using API.Contracts;
using API.Contracts.Results;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PenaltyPointsController : ControllerBase
{
    private readonly IPenaltyPointsService _penaltyPointsService;

    public PenaltyPointsController(IPenaltyPointsService penaltyPointsService)
    {
        _penaltyPointsService = penaltyPointsService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PenaltyPointsReadModel>> Get()
    {
        var result = _penaltyPointsService.GetAllPenaltyPoints();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<PenaltyPointsReadModel> GetById(int id)
    {
        var result = _penaltyPointsService.GetPenaltyPointById(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpGet("{score}")]
    public ActionResult<OperationResult<PenaltyPointsReadModel>>GetPenaltyPointsByScore(decimal score)
    {
        var result = _penaltyPointsService.GetPenaltyPointsByScore(score);
        return Ok(result);
    }

}