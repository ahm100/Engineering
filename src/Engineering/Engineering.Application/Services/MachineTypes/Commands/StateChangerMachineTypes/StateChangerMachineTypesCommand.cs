using MachineType = Engineering.Domain.Entities.MachineTypes.MachineType;

namespace Engineering.Application.Services.MachineTypes.Commands.StateChangerMachineTypes;

public record StateChangerMachineTypesCommand(
    List<MachineType> Items,
    bool State
    ) : ICommand<bool?>;
