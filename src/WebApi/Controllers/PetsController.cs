using Application.DTOs.Pets;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    private readonly IPetService _petsService;

    public PetsController(IPetService petsService)
    {
        _petsService = petsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _petsService.GetAllAsync());
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var pet = await _petsService.GetPetById(id);
        return pet is null ? NotFound() : Ok(pet);
    }

    [HttpGet("client/{clientId:long}")]
    public async Task<IActionResult> GetByClientId(long clientId)
    {
        return Ok(await _petsService.GetByClientIdAsync(clientId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePetDto dto)
    {
        try
        {
            var created = await _petsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
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
    public async Task<IActionResult> Update(long id, [FromBody] UpdatePetDto dto)
    {
        if (id != dto.PetId)
            return BadRequest();

        try
        {
            await _petsService.UpdateAsync(dto);
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
            await _petsService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
