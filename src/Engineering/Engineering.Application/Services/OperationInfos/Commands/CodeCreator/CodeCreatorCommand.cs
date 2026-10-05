
namespace Engineering.Application.Services.OperationInfos.Commands.CodeCreator;

public record CodeCreatorCommand(
    long? CompanyId
    ) : ICommand<string?>;
