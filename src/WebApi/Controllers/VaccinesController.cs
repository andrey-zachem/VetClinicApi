using Application.DTOs.Vaccines;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class VaccinesController : ControllerBase
{
    private readonly IVaccinesService _vaccinesService;

    public VaccinesController(IVaccinesService vaccinesService)
    {
        _vaccinesService = vaccinesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _vaccinesService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vaccine = await _vaccinesService.GetByIdAsync(id);
        return vaccine is null ? NotFound() : Ok(vaccine);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VaccineDto dto)
    {
        try
        {
            var created = await _vaccinesService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.VaccineId }, created);
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
    public async Task<IActionResult> Update(int id, [FromBody] VaccineDto dto)
    {
        if (id != dto.VaccineId)
            return BadRequest();

        try
        {
            await _vaccinesService.UpdateAsync(dto);
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
            await _vaccinesService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
