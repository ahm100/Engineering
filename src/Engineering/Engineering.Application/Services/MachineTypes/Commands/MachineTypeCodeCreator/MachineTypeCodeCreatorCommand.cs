
namespace Engineering.Application.Services.MachineTypes.Commands.MachineTypeCodeCreator;

public record MachineTypeCodeCreatorCommand(long? CompanyId
    ) : ICommand<string?>;
