using Application.DTOs.Files;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AttachmentKindsController : ControllerBase
{
    private readonly IAttachmentKindsService _attachmentKindsService;

    public AttachmentKindsController(IAttachmentKindsService attachmentKindsService)
    {
        _attachmentKindsService = attachmentKindsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _attachmentKindsService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(short id)
    {
        var kind = await _attachmentKindsService.GetByIdAsync(id);
        return kind is null ? NotFound() : Ok(kind);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AttachmentKindDto dto)
    {
        try
        {
            var created = await _attachmentKindsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.KindId }, created);
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
    public async Task<IActionResult> Update(short id, [FromBody] AttachmentKindDto dto)
    {
        if (id != dto.KindId)
            return BadRequest();

        try
        {
            await _attachmentKindsService.UpdateAsync(dto);
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
            await _attachmentKindsService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
