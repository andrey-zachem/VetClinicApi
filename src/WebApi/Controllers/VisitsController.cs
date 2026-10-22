using Application.DTOs.Visits;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class VisitsController : ControllerBase
{
    private readonly IVisitsService _visitsService;

    public VisitsController(IVisitsService visitsService)
    {
        _visitsService = visitsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _visitsService.GetAllAsync());
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var visit = await _visitsService.GetByIdAsync(id);
        return visit is null ? NotFound() : Ok(visit);
    }

    [HttpGet("pet/{petId:long}")]
    public async Task<IActionResult> GetByPetId(long petId)
    {
        return Ok(await _visitsService.GetByPetIdAsync(petId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VisitCreateDto dto)
    {
        try
        {
            var created = await _visitsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.VisitId }, created);
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

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] VisitDto dto)
    {
        if (id != dto.VisitId)
            return BadRequest();

        try
        {
            await _visitsService.UpdateAsync(dto);
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

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            await _visitsService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
