using Microsoft.AspNetCore.Mvc;
using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;
using NewsAndWeatherAPI.Services;

namespace NewsAndWeatherAPI.Controllers;


[Route("api/categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;
    
    public CategoriesController(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }
    
    [HttpGet]
    public ActionResult<List<CategoryGetDto>> GetAll()
    {
        List<CategoryGetDto> listOfCategories = _categoriesService.GetAll();
        if (listOfCategories?.Count == 0)
        {
            return NotFound("This don't have categories");
        }
        else if (listOfCategories is null)
        {
            return BadRequest("Something went wrong");
        }
        return Ok(listOfCategories);
    }

    [Route("{id}")]
    [HttpGet]
    public ActionResult<CategoryGetDto> GetById([FromRoute]int id)
    {
        CategoryGetDto category = _categoriesService.GetById(id);
        if (category is null)
        {
            return NotFound("This category not exist");
        }
        return Ok(category);
    }
}