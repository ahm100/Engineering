namespace Engineering.Application.Services.CostCenterTypes.Commands.CodeCreator;

public record CodeCreatorCommand(
    long? CompanyId)
    : ICommand<string?>;