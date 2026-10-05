using MachineType = Engineering.Domain.Entities.MachineTypes.MachineType;

namespace Engineering.Application.Services.MachineTypes.Commands.DisableMachineType;

public record DisableMachineTypeCommand(
long Id
    ) : ICommand<MachineType>;
