using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Services.MachineTypes.Commands.ActiveMachineType;

public record ActiveMachineTypeCommand(
    long Id
    ) : ICommand<MachineType>;