using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Commands.InactiveMachineriesGroup;

public record InactiveMachineriesGroupCommand(
    long Id
    ) : ICommand<MachineriesGroup>;