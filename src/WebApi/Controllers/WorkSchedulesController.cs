using Application.DTOs.Employees.WorkSchedules;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class WorkSchedulesController : ControllerBase
{
    private readonly IWorkScheduleService _workScheduleService;

    public WorkSchedulesController(IWorkScheduleService workScheduleService)
    {
        _workScheduleService = workScheduleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _workScheduleService.GetAllAsync());
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var schedule = await _workScheduleService.GetByIdAsync(id);
        return schedule is null ? NotFound() : Ok(schedule);
    }

    [HttpGet("employee/{employeeId:long}")]
    public async Task<IActionResult> GetByEmployeeId(long employeeId)
    {
        return Ok(await _workScheduleService.GetByEmployeeIdAsync(employeeId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WorkScheduleCreate dto)
    {
        try
        {
            var created = await _workScheduleService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ScheduleId }, created);
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
    public async Task<IActionResult> Update(long id, [FromBody] WorkScheduleDto dto)
    {
        if (id != dto.ScheduleId)
            return BadRequest();

        try
        {
            await _workScheduleService.UpdateAsync(dto);
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
            await _workScheduleService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
