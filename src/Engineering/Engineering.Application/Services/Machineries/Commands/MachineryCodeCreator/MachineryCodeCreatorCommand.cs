
namespace Engineering.Application.Services.Machineries.Commands.MachineryCodeCreator;

public record MachineryCodeCreatorCommand(
    long? CompanyId
    ) : ICommand<string?>;
