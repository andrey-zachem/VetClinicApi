using Application.DTOs.Species;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class SpeciesController : ControllerBase
{
    private readonly ISpeciesService _speciesService;

    public SpeciesController(ISpeciesService speciesService)
    {
        _speciesService = speciesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _speciesService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(short id)
    {
        var species = await _speciesService.GetByIdAsync(id);
        return species is null ? NotFound() : Ok(species);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SpeciesDao dto)
    {
        try
        {
            var created = await _speciesService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.SpeciesId }, created);
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
    public async Task<IActionResult> Update(short id, [FromBody] SpeciesDao dto)
    {
        if (id != dto.SpeciesId)
            return BadRequest();

        try
        {
            await _speciesService.UpdateAsync(dto);
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
            await _speciesService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
