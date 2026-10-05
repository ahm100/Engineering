using CabinType = Engineering.Domain.Entities.MachineTypes.CabinType;

namespace Engineering.Application.Services.CabinTypes.Commands.ActiveCabinType;

public record ActiveCabinTypeCommand(
    long Id)
    : ICommand<CabinType>;