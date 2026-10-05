using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Commands.DisableMachineriesGroup;

public record DisableMachineriesGroupCommand(
    long Id
    ) : ICommand<MachineriesGroup>;