using Application.DTOs.Diagnoses;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class DiagnosesController : ControllerBase
{
    private readonly IDiagnosesService _diagnosesService;

    public DiagnosesController(IDiagnosesService diagnosesService)
    {
        _diagnosesService = diagnosesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _diagnosesService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var diagnosis = await _diagnosesService.GetByIdAsync(id);
        return diagnosis is null ? NotFound() : Ok(diagnosis);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DiagnosisDto dto)
    {
        try
        {
            var created = await _diagnosesService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.DiagnosisId }, created);
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
    public async Task<IActionResult> Update(int id, [FromBody] DiagnosisDto dto)
    {
        if (id != dto.DiagnosisId)
            return BadRequest();

        try
        {
            await _diagnosesService.UpdateAsync(dto);
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
            await _diagnosesService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
