using Application.DTOs.ClinicServices;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ServiceCategoriesController : ControllerBase
{
    private readonly IServiceCategoriesService _serviceCategoriesService;

    public ServiceCategoriesController(IServiceCategoriesService serviceCategoriesService)
    {
        _serviceCategoriesService = serviceCategoriesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _serviceCategoriesService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(short id)
    {
        var category = await _serviceCategoriesService.GetByIdAsync(id);
        return category is null ? NotFound() : Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ServiceCategoryDto dto)
    {
        try
        {
            var created = await _serviceCategoriesService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.CategoryId }, created);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(short id, [FromBody] ServiceCategoryDto dto)
    {
        if (id != dto.CategoryId)
            return BadRequest();

        try
        {
            await _serviceCategoriesService.UpdateAsync(dto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(short id)
    {
        try
        {
            await _serviceCategoriesService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
