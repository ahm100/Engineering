using Engineering.Domain.Entities.Machineries;

namespace Engineering.Application.Services.MachineriesGroups.Commands.StateChangerMachineriesGroups;

public record StateChangerMachineriesGroupsCommand(
    List<MachineriesGroup> Items,
    bool State
    ) : ICommand<bool?>;
