using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Commands.DisableMachinery;

public record DisableMachineryCommand(
    long Id
    ) : ICommand<Machinery>;