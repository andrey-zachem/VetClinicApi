using Application.DTOs.Files;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class FileObjectsController : ControllerBase
{
    private readonly IFileObjectsService _fileObjectsService;

    public FileObjectsController(IFileObjectsService fileObjectsService)
    {
        _fileObjectsService = fileObjectsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _fileObjectsService.GetAllAsync());
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var file = await _fileObjectsService.GetByIdAsync(id);
        return file is null ? NotFound() : Ok(file);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FileObjectCreateDto dto)
    {
        try
        {
            var created = await _fileObjectsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.FileId }, created);
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
            await _fileObjectsService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
