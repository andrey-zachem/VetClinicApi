using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AuditLogsController : ControllerBase
{
    private readonly IAuditLogService _auditLogService;

    public AuditLogsController(IAuditLogService auditLogService)
    {
        _auditLogService = auditLogService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _auditLogService.GetAllAsync());
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var entry = await _auditLogService.GetByIdAsync(id);
        return entry is null ? NotFound() : Ok(entry);
    }

    [HttpGet("table/{tableName}")]
    public async Task<IActionResult> GetByTable(string tableName)
    {
        return Ok(await _auditLogService.GetByTableAsync(tableName));
    }
}
