using CabinType = Engineering.Domain.Entities.MachineTypes.CabinType;

namespace Engineering.Application.Services.CabinTypes.Commands.StateChangerCabinTypes;

public record StateChangerCabinTypesCommand(
    List<CabinType> Items,
    bool State)
    : ICommand<bool?>;