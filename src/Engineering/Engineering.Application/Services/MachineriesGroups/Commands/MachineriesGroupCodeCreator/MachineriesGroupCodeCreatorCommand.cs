
namespace Engineering.Application.Services.MachineriesGroups.Commands.MachineriesGroupCodeCreator;

public record MachineriesGroupCodeCreatorCommand(long? CompanyId
    ) : ICommand<string?>;
