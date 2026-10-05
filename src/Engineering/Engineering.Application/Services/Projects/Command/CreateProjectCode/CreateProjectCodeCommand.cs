namespace Engineering.Application.Services.Projects.Commands.CreateProjectCode;

public record CreateProjectCodeCommand(
    string EmployerSymbol,
    string? CostCenterCode,
    long ProjectCode
    ) : ICommand<string>;