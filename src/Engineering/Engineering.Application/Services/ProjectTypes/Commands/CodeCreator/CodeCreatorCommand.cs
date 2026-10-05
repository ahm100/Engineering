
namespace Engineering.Application.Services.ProjectTypes.Commands.CodeCreator;

public record CodeCreatorCommand(
    long? CompanyId
    ) : ICommand<string?>;