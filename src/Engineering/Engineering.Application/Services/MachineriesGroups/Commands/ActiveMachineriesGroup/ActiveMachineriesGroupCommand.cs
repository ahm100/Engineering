using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Commands.ActiveMachineriesGroup;

public record ActiveMachineriesGroupCommand(
    long Id
    ) : ICommand<MachineriesGroup>;