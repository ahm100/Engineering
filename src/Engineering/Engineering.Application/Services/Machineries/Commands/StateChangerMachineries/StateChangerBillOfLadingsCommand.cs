using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Commands.StateChangerMachineries;

public record StateChangerMachineriesCommand(
    List<Machinery> Items,
    bool State
    ) : ICommand<bool?>;
