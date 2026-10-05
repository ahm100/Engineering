using CabinType = Engineering.Domain.Entities.MachineTypes.CabinType;

namespace Engineering.Application.Services.CabinTypes.Commands.DeleteCabinType;

public record DeleteCabinTypeCommand(
    long Id)
    : ICommand<CabinType>;