using Application.DTOs.Employees;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class EmployeesService : IEmployeesService
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IPositionsRepository _positionsRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISpecialtiesRepository _specialtiesRepository;
    private readonly IEmployeeSpecialtiesRepository _employeeSpecialtiesRepository;

    public EmployeesService(
        IEmployeesRepository employeesRepository,
        IPositionsRepository positionsRepository,
        IUserRepository userRepository,
        ISpecialtiesRepository specialtiesRepository,
        IEmployeeSpecialtiesRepository employeeSpecialtiesRepository)
    {
        _employeesRepository = employeesRepository;
        _positionsRepository = positionsRepository;
        _userRepository = userRepository;
        _specialtiesRepository = specialtiesRepository;
        _employeeSpecialtiesRepository = employeeSpecialtiesRepository;
    }

    public async Task<EmployeesDto?> GetEmployeeByIdAsync(long id)
    {
        var employee = await _employeesRepository.GetByIdAsync(id);
        return employee is null ? null : MapToDto(employee);
    }

    public async Task<IEnumerable<EmployeesDto>> GetAllEmployeesAsync()
    {
        var employees = await _employeesRepository.GetAllAsync();
        return employees.Select(MapToDto);
    }

    public async Task<EmployeesDto> CreateEmployeeAsync(EmployeesCreate employee)
    {
        await EnsurePositionExists(employee.PositionId);
        await EnsureUserExists(employee.UserId);
        await EnsureSpecialtiesExist(employee.SpecialtyIds);

        var newEmployee = new Employees
        {
            UserId = employee.UserId,
            PositionId = employee.PositionId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            MiddleName = employee.MiddleName,
            Email = employee.Email,
            Phone = employee.Phone,
            HiredOn = employee.HiredOn,
            FiredOn = employee.FiredOn
        };

        foreach (var specialtyId in employee.SpecialtyIds.Distinct())
        {
            newEmployee.EmployeeSpecialties.Add(new EmployeeSpecialties
            {
                SpecialtyId = specialtyId
            });
        }

        await _employeesRepository.AddAsync(newEmployee);
        var created = await _employeesRepository.GetByIdAsync(newEmployee.EmployeeId);
        return MapToDto(created ?? newEmployee);
    }

    public async Task UpdateEmployeeAsync(EmployeesDto employee)
    {
        var existingEmployee = await _employeesRepository.GetByIdAsync(employee.EmployeeId);
        if (existingEmployee == null)
            throw new KeyNotFoundException("Employee not found");

        await EnsurePositionExists(employee.PositionId);
        await EnsureUserExists(employee.UserId);
        await EnsureSpecialtiesExist(employee.SpecialtyIds);

        existingEmployee.UserId = employee.UserId;
        existingEmployee.PositionId = employee.PositionId;
        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;
        existingEmployee.MiddleName = employee.MiddleName;
        existingEmployee.Email = employee.Email;
        existingEmployee.Phone = employee.Phone;
        existingEmployee.HiredOn = employee.HiredOn;
        existingEmployee.FiredOn = employee.FiredOn;

        await _employeesRepository.UpdateAsync(existingEmployee);
        await _employeeSpecialtiesRepository.ReplaceForEmployeeAsync(employee.EmployeeId, employee.SpecialtyIds);
    }

    public async Task DeleteEmployeeAsync(long id)
    {
        var existingEmployee = await _employeesRepository.GetByIdAsync(id);
        if (existingEmployee == null)
            throw new KeyNotFoundException("Employee not found");
        await _employeesRepository.DeleteAsync(id);
    }

    private async Task EnsurePositionExists(short positionId)
    {
        var position = await _positionsRepository.GetByIdAsync(positionId);
        if (position == null)
            throw new KeyNotFoundException($"Position {positionId} not found");
    }

    private async Task EnsureUserExists(long? userId)
    {
        if (userId is null)
            return;

        var user = await _userRepository.GetByIdAsync(userId.Value);
        if (user == null)
            throw new KeyNotFoundException($"User {userId} not found");
    }

    private async Task EnsureSpecialtiesExist(IEnumerable<short> specialtyIds)
    {
        foreach (var specialtyId in specialtyIds.Distinct())
        {
            var specialty = await _specialtiesRepository.GetByIdAsync(specialtyId);
            if (specialty == null)
                throw new KeyNotFoundException($"Specialty {specialtyId} not found");
        }
    }

    private static EmployeesDto MapToDto(Employees employee) => new()
    {
        EmployeeId = employee.EmployeeId,
        UserId = employee.UserId,
        PositionId = employee.PositionId,
        PositionName = employee.Position?.Name,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        MiddleName = employee.MiddleName,
        Email = employee.Email,
        Phone = employee.Phone,
        HiredOn = employee.HiredOn,
        FiredOn = employee.FiredOn,
        SpecialtyIds = employee.EmployeeSpecialties.Select(es => es.SpecialtyId).ToList()
    };
}
