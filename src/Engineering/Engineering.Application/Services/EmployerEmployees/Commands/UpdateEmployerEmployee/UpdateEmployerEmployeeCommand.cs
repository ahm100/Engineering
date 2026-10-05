using Engineering.Domain.Entities.EmployerEmployees;

namespace Engineering.Application.Services.EmployerEmployees.Commands.UpdateEmployerEmployee;

public record UpdateEmployerEmployeeCommand(
    long Id,
    long? EmployerId,
    bool? IsActive) : ICommand<EmployerEmployee>;