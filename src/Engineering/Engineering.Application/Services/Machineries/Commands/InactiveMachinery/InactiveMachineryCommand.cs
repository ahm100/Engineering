using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Commands.InactiveMachinery;

public record InactiveMachineryCommand(
    long Id
    ) : ICommand<Machinery>;