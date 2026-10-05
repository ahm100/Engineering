
namespace Engineering.Application.Services.CostCenters.Commands.CodeCreator;

public record CodeCreatorCommand(
    long? CompanyId
    ) : ICommand<string?>;