namespace Application.DTOs.ClinicServices;

public class ClinicServiceDto
{
    public int ServiceId { get; set; }
    public short CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public short? DurationMin { get; set; }
    public bool IsActive { get; set; } = true;
}
