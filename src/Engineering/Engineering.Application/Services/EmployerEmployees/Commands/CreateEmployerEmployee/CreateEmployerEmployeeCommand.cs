using Engineering.Domain.Entities.EmployerEmployees;

namespace Engineering.Application.Services.EmployerEmployees.Commands.CreateEmployerEmployee;

public record CreateEmployerEmployeeCommand(long EmployeeId,
    long EmployerId,
    bool IsActive) : ICommand<EmployerEmployee>;