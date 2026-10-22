using Application.DTOs.Employees.Positions;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PositionsController : ControllerBase
{
    private readonly IPositionsService _positionsService;

    public PositionsController(IPositionsService positionsService)
    {
        _positionsService = positionsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _positionsService.GetAllPositionsAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(short id)
    {
        var position = await _positionsService.GetPositionByIdAsync(id);
        return position is null ? NotFound() : Ok(position);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PositionsDto position)
    {
        try
        {
            var created = await _positionsService.CreatePositionAsync(position);
            return CreatedAtAction(nameof(GetById), new { id = created.PositionId }, created);
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
    public async Task<IActionResult> Update(short id, [FromBody] PositionsDto position)
    {
        if (id != position.PositionId)
            return BadRequest();

        try
        {
            await _positionsService.UpdatePositionAsync(position);
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
            await _positionsService.DeletePositionAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
