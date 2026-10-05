namespace Engineering.Application.Services.EmployerEmployees.Commands.DeleteEmployerEmployee;

public record DeleteEmployerEmployeeCommand(
    long Id) : ICommand<bool?>;