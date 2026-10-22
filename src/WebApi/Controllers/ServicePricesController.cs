using Application.DTOs.ClinicServices;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ServicePricesController : ControllerBase
{
    private readonly IServicePricesService _servicePricesService;

    public ServicePricesController(IServicePricesService servicePricesService)
    {
        _servicePricesService = servicePricesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _servicePricesService.GetAllAsync());
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var price = await _servicePricesService.GetByIdAsync(id);
        return price is null ? NotFound() : Ok(price);
    }

    [HttpGet("service/{serviceId:int}")]
    public async Task<IActionResult> GetByServiceId(int serviceId)
    {
        return Ok(await _servicePricesService.GetByServiceIdAsync(serviceId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ServicePriceDto dto)
    {
        try
        {
            var created = await _servicePricesService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PriceId }, created);
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
    public async Task<IActionResult> Update(long id, [FromBody] ServicePriceDto dto)
    {
        if (id != dto.PriceId)
            return BadRequest();

        try
        {
            await _servicePricesService.UpdateAsync(dto);
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
            await _servicePricesService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
