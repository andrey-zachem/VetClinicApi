using Application.DTOs.Employees.Specialties;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class SpecialtiesController : ControllerBase
{
    private readonly ISpecialtiesService _specialtiesService;

    public SpecialtiesController(ISpecialtiesService specialtiesService)
    {
        _specialtiesService = specialtiesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _specialtiesService.GetAllSpecialtiesAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(short id)
    {
        var specialty = await _specialtiesService.GetSpecialtyByIdAsync(id);
        return specialty is null ? NotFound() : Ok(specialty);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SpecialtiesDto specialty)
    {
        try
        {
            var created = await _specialtiesService.CreateSpecialtyAsync(specialty);
            return CreatedAtAction(nameof(GetById), new { id = created.SpecialtyId }, created);
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
    public async Task<IActionResult> Update(short id, [FromBody] SpecialtiesDto specialty)
    {
        if (id != specialty.SpecialtyId)
            return BadRequest();

        try
        {
            await _specialtiesService.UpdateSpecialtyAsync(specialty);
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
            await _specialtiesService.DeleteSpecialtyAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
