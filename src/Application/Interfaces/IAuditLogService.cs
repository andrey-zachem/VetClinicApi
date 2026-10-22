using Application.DTOs.Audit;

namespace Application.Interfaces;

public interface IAuditLogService
{
    Task<IEnumerable<AuditLogDto>> GetAllAsync();
    Task<AuditLogDto?> GetByIdAsync(long id);
    Task<IEnumerable<AuditLogDto>> GetByTableAsync(string tableName);
}
