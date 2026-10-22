using Application.DTOs.Appointments;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AppointmentStatusesController : ControllerBase
{
    private readonly IAppointmentStatusesService _appointmentStatusesService;

    public AppointmentStatusesController(IAppointmentStatusesService appointmentStatusesService)
    {
        _appointmentStatusesService = appointmentStatusesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _appointmentStatusesService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(short id)
    {
        var status = await _appointmentStatusesService.GetByIdAsync(id);
        return status is null ? NotFound() : Ok(status);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AppointmentStatusDto dto)
    {
        try
        {
            var created = await _appointmentStatusesService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.StatusId }, created);
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
    public async Task<IActionResult> Update(short id, [FromBody] AppointmentStatusDto dto)
    {
        if (id != dto.StatusId)
            return BadRequest();

        try
        {
            await _appointmentStatusesService.UpdateAsync(dto);
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
            await _appointmentStatusesService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
