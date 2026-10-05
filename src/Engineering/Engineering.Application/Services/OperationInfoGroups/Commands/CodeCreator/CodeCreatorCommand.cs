
namespace Engineering.Application.Services.OperationInfoGroups.Commands.CodeCreator;

public record CodeCreatorCommand(
    long? CompanyId
    ) : ICommand<string?>;