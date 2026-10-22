using Application.DTOs.Breeds;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class BreedsController : ControllerBase
{
    private readonly IBreedsService _breedsService;

    public BreedsController(IBreedsService breedsService)
    {
        _breedsService = breedsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _breedsService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var breed = await _breedsService.GetByIdAsync(id);
        return breed is null ? NotFound() : Ok(breed);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BreedCreateDto dto)
    {
        try
        {
            var created = await _breedsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.BreedId }, created);
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
    public async Task<IActionResult> Update(int id, [FromBody] BreedsDao dto)
    {
        if (id != dto.BreedId)
            return BadRequest();

        try
        {
            await _breedsService.UpdateAsync(dto);
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
            await _breedsService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
