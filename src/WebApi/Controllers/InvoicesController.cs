using Application.DTOs.Invoices;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoicesService _invoicesService;

    public InvoicesController(IInvoicesService invoicesService)
    {
        _invoicesService = invoicesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _invoicesService.GetAllAsync());
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var invoice = await _invoicesService.GetByIdAsync(id);
        return invoice is null ? NotFound() : Ok(invoice);
    }

    [HttpGet("client/{clientId:long}")]
    public async Task<IActionResult> GetByClientId(long clientId)
    {
        return Ok(await _invoicesService.GetByClientIdAsync(clientId));
    }

    [HttpGet("visit/{visitId:long}")]
    public async Task<IActionResult> GetByVisitId(long visitId)
    {
        return Ok(await _invoicesService.GetByVisitIdAsync(visitId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] InvoiceCreateDto dto)
    {
        try
        {
            var created = await _invoicesService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.InvoiceId }, created);
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
            await _invoicesService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
