using Application.DTOs.Prescriptions;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionsService _prescriptionsService;

    public PrescriptionsController(IPrescriptionsService prescriptionsService)
    {
        _prescriptionsService = prescriptionsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _prescriptionsService.GetAllAsync());
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var prescription = await _prescriptionsService.GetByIdAsync(id);
        return prescription is null ? NotFound() : Ok(prescription);
    }

    [HttpGet("visit/{visitId:long}")]
    public async Task<IActionResult> GetByVisitId(long visitId)
    {
        return Ok(await _prescriptionsService.GetByVisitIdAsync(visitId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PrescriptionDto dto)
    {
        try
        {
            var created = await _prescriptionsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PrescriptionId }, created);
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
    public async Task<IActionResult> Update(long id, [FromBody] PrescriptionDto dto)
    {
        if (id != dto.PrescriptionId)
            return BadRequest();

        try
        {
            await _prescriptionsService.UpdateAsync(dto);
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
            await _prescriptionsService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
