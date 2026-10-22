using Application.DTOs.Invoices;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PaymentMethodsController : ControllerBase
{
    private readonly IPaymentMethodsService _paymentMethodsService;

    public PaymentMethodsController(IPaymentMethodsService paymentMethodsService)
    {
        _paymentMethodsService = paymentMethodsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _paymentMethodsService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(short id)
    {
        var method = await _paymentMethodsService.GetByIdAsync(id);
        return method is null ? NotFound() : Ok(method);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PaymentMethodDto dto)
    {
        try
        {
            var created = await _paymentMethodsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.MethodId }, created);
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
    public async Task<IActionResult> Update(short id, [FromBody] PaymentMethodDto dto)
    {
        if (id != dto.MethodId)
            return BadRequest();

        try
        {
            await _paymentMethodsService.UpdateAsync(dto);
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
            await _paymentMethodsService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
