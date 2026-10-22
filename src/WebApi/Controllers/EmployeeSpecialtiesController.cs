using Application.DTOs.Employees.Specialties;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class EmployeeSpecialtiesController : ControllerBase
{
    private readonly IEmployeeSpecialtiesService _employeeSpecialtiesService;

    public EmployeeSpecialtiesController(IEmployeeSpecialtiesService employeeSpecialtiesService)
    {
        _employeeSpecialtiesService = employeeSpecialtiesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _employeeSpecialtiesService.GetAllAsync());
    }

    [HttpGet("employee/{employeeId:long}")]
    public async Task<IActionResult> GetByEmployeeId(long employeeId)
    {
        return Ok(await _employeeSpecialtiesService.GetByEmployeeIdAsync(employeeId));
    }

    [HttpPost]
    public async Task<IActionResult> Assign([FromBody] EmployeeSpecialtyDto dto)
    {
        try
        {
            var created = await _employeeSpecialtiesService.AssignAsync(dto);
            return CreatedAtAction(
                nameof(GetByEmployeeId),
                new { employeeId = created.EmployeeId },
                created);
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

    [HttpDelete("{employeeId:long}/{specialtyId:int}")]
    public async Task<IActionResult> Unassign(long employeeId, short specialtyId)
    {
        try
        {
            await _employeeSpecialtiesService.UnassignAsync(employeeId, specialtyId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
