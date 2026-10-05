using MachineType = Engineering.Domain.Entities.MachineTypes.MachineType;

namespace Engineering.Application.Services.MachineTypes.Commands.InactiveMachineType;

public record InactiveMachineTypeCommand(
    long Id
    ) : ICommand<MachineType>;