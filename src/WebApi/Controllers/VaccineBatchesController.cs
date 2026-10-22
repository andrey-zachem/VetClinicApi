using Application.DTOs.Vaccines;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class VaccineBatchesController : ControllerBase
{
    private readonly IVaccineBatchesService _vaccineBatchesService;

    public VaccineBatchesController(IVaccineBatchesService vaccineBatchesService)
    {
        _vaccineBatchesService = vaccineBatchesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _vaccineBatchesService.GetAllAsync());
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var batch = await _vaccineBatchesService.GetByIdAsync(id);
        return batch is null ? NotFound() : Ok(batch);
    }

    [HttpGet("vaccine/{vaccineId:int}")]
    public async Task<IActionResult> GetByVaccineId(int vaccineId)
    {
        return Ok(await _vaccineBatchesService.GetByVaccineIdAsync(vaccineId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VaccineBatchDto dto)
    {
        try
        {
            var created = await _vaccineBatchesService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.BatchId }, created);
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
    public async Task<IActionResult> Update(long id, [FromBody] VaccineBatchDto dto)
    {
        if (id != dto.BatchId)
            return BadRequest();

        try
        {
            await _vaccineBatchesService.UpdateAsync(dto);
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
            await _vaccineBatchesService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
