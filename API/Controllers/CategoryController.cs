using API.Contracts;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CategoryReadModel>> Get()
    {
        var result = _categoryService.GetAllCategories();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<CategoryReadModel> GetById(int id)
    {
        var result = _categoryService.GetCategoryById(id);
        if (result.IsSuccess) return Ok(result.Data);
        return Conflict(result.ErrorMessage);
    }
    
}