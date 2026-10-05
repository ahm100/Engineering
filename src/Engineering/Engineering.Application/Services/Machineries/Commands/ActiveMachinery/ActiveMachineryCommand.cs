using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Commands.ActiveMachinery;

public record ActiveMachineryCommand(
    long Id
    ) : ICommand<Machinery>;