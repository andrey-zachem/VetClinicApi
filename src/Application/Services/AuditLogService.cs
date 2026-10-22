using Application.DTOs.Audit;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class AuditLogService : IAuditLogService
{
    private readonly IAuditLogRepository _auditLog;

    public AuditLogService(IAuditLogRepository auditLog)
    {
        _auditLog = auditLog;
    }

    public async Task<IEnumerable<AuditLogDto>> GetAllAsync()
        => (await _auditLog.GetAllAsync()).Select(Map);

    public async Task<AuditLogDto?> GetByIdAsync(long id)
    {
        var entity = await _auditLog.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<IEnumerable<AuditLogDto>> GetByTableAsync(string tableName)
        => (await _auditLog.GetByTableAsync(tableName)).Select(Map);

    private static AuditLogDto Map(AuditLog entry) => new()
    {
        AuditId = entry.AuditId,
        UserId = entry.UserId,
        Action = entry.Action,
        TableName = entry.TableName,
        RecordId = entry.RecordId,
        Changes = entry.Changes,
        CreatedAt = entry.CreatedAt
    };
}
