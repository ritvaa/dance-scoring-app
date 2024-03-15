using API.Contracts;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DancerController : ControllerBase
{
    private readonly IDancerService _dancerService;

    public DancerController(IDancerService dancerService)
    {
        _dancerService = dancerService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DancerReadModel>> Get()
    {
        var dancers = _dancerService.GetAllDancers();
        return Ok(dancers);
    }

    [HttpGet("{id}")]
    public ActionResult<DancerReadModel> GetById(Guid id)
    {
        var result = _dancerService.GetDancerById(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPost]
    public IActionResult CreateDancer(DancerWriteModel dancer)
    {
        var result = _dancerService.CreateDancer(dancer);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPut("{id}")]
    public ActionResult<DancerReadModel> Update(Guid id, DancerWriteModel dancer)
    {
        var result = _dancerService.UpdateDancer(id, dancer);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteDancer(Guid id)
    {
        var result = _dancerService.DeleteDancer(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }

    [HttpPost("import")]
    public IActionResult ImportDancers(IFormFile file)
    {
        try
        {
            if (file.Length > 0)
                using (var stream = file.OpenReadStream())
                {
                    var dancers = _dancerService.ImportDancersFromCsv(stream);
                    return Ok(dancers);
                }

            return BadRequest("No file uploaded.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while importing dancers: {ex.Message}");
        }
    }
}