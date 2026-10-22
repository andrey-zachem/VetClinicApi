using Application.DTOs.Vaccines;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class VaccinationsController : ControllerBase
{
    private readonly IVaccinationsService _vaccinationsService;

    public VaccinationsController(IVaccinationsService vaccinationsService)
    {
        _vaccinationsService = vaccinationsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _vaccinationsService.GetAllAsync());
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var vaccination = await _vaccinationsService.GetByIdAsync(id);
        return vaccination is null ? NotFound() : Ok(vaccination);
    }

    [HttpGet("pet/{petId:long}")]
    public async Task<IActionResult> GetByPetId(long petId)
    {
        return Ok(await _vaccinationsService.GetByPetIdAsync(petId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VaccinationDto dto)
    {
        try
        {
            var created = await _vaccinationsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.VaccinationId }, created);
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
    public async Task<IActionResult> Update(long id, [FromBody] VaccinationDto dto)
    {
        if (id != dto.VaccinationId)
            return BadRequest();

        try
        {
            await _vaccinationsService.UpdateAsync(dto);
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
            await _vaccinationsService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
