using Application.DTOs.Employees.Specialties;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class EmployeeSpecialtiesService : IEmployeeSpecialtiesService
{
    private readonly IEmployeeSpecialtiesRepository _employeeSpecialtiesRepository;
    private readonly IEmployeesRepository _employeesRepository;
    private readonly ISpecialtiesRepository _specialtiesRepository;

    public EmployeeSpecialtiesService(
        IEmployeeSpecialtiesRepository employeeSpecialtiesRepository,
        IEmployeesRepository employeesRepository,
        ISpecialtiesRepository specialtiesRepository)
    {
        _employeeSpecialtiesRepository = employeeSpecialtiesRepository;
        _employeesRepository = employeesRepository;
        _specialtiesRepository = specialtiesRepository;
    }

    public async Task<IEnumerable<EmployeeSpecialtyDto>> GetAllAsync()
    {
        var items = await _employeeSpecialtiesRepository.GetAllAsync();
        return items.Select(MapToDto);
    }

    public async Task<IEnumerable<EmployeeSpecialtyDto>> GetByEmployeeIdAsync(long employeeId)
    {
        var items = await _employeeSpecialtiesRepository.GetByEmployeeIdAsync(employeeId);
        return items.Select(MapToDto);
    }

    public async Task<EmployeeSpecialtyDto> AssignAsync(EmployeeSpecialtyDto dto)
    {
        var employee = await _employeesRepository.GetByIdAsync(dto.EmployeeId);
        if (employee == null)
            throw new KeyNotFoundException("Employee not found");

        var specialty = await _specialtiesRepository.GetByIdAsync(dto.SpecialtyId);
        if (specialty == null)
            throw new KeyNotFoundException("Specialty not found");

        var existing = await _employeeSpecialtiesRepository.GetByIdAsync(dto.EmployeeId, dto.SpecialtyId);
        if (existing != null)
            return MapToDto(existing);

        var entity = new EmployeeSpecialties
        {
            EmployeeId = dto.EmployeeId,
            SpecialtyId = dto.SpecialtyId
        };
        await _employeeSpecialtiesRepository.AddAsync(entity);
        return MapToDto(entity);
    }

    public async Task UnassignAsync(long employeeId, short specialtyId)
    {
        var existing = await _employeeSpecialtiesRepository.GetByIdAsync(employeeId, specialtyId);
        if (existing == null)
            throw new KeyNotFoundException("Employee specialty not found");

        await _employeeSpecialtiesRepository.DeleteAsync(employeeId, specialtyId);
    }

    private static EmployeeSpecialtyDto MapToDto(EmployeeSpecialties entity) => new()
    {
        EmployeeId = entity.EmployeeId,
        SpecialtyId = entity.SpecialtyId
    };
}
