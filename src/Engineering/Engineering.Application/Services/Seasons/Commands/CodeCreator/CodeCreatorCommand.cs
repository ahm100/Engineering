
namespace Engineering.Application.Services.Seasons.Commands.CodeCreator;

public record CodeCreatorCommand(
    long? CompanyId
    ) : ICommand<string?>;