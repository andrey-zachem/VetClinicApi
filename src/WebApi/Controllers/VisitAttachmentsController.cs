using Application.DTOs.Files;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class VisitAttachmentsController : ControllerBase
{
    private readonly IVisitAttachmentsService _visitAttachmentsService;

    public VisitAttachmentsController(IVisitAttachmentsService visitAttachmentsService)
    {
        _visitAttachmentsService = visitAttachmentsService;
    }

    [HttpGet("visit/{visitId:long}")]
    public async Task<IActionResult> GetByVisitId(long visitId)
    {
        return Ok(await _visitAttachmentsService.GetByVisitIdAsync(visitId));
    }

    [HttpPost]
    public async Task<IActionResult> Attach([FromBody] VisitAttachmentDto dto)
    {
        try
        {
            var created = await _visitAttachmentsService.AttachAsync(dto);
            return CreatedAtAction(
                nameof(GetByVisitId),
                new { visitId = created.VisitId },
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

    [HttpDelete("{visitId:long}/{fileId:long}")]
    public async Task<IActionResult> Detach(long visitId, long fileId)
    {
        try
        {
            await _visitAttachmentsService.DetachAsync(visitId, fileId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
