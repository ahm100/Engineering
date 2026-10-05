using CabinType = Engineering.Domain.Entities.MachineTypes.CabinType;

namespace Engineering.Application.Services.CabinTypes.Commands.InactiveCabinType;

public record InactiveCabinTypeCommand(
    long Id)
    : ICommand<CabinType>;