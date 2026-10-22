using Application.DTOs.Medications;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class MedicationsController : ControllerBase
{
    private readonly IMedicationsService _medicationsService;

    public MedicationsController(IMedicationsService medicationsService)
    {
        _medicationsService = medicationsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _medicationsService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var medication = await _medicationsService.GetByIdAsync(id);
        return medication is null ? NotFound() : Ok(medication);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MedicationDto dto)
    {
        try
        {
            var created = await _medicationsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.MedicationId }, created);
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
    public async Task<IActionResult> Update(int id, [FromBody] MedicationDto dto)
    {
        if (id != dto.MedicationId)
            return BadRequest();

        try
        {
            await _medicationsService.UpdateAsync(dto);
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
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _medicationsService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
