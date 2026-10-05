using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Commands.CreateMachineriesGroup;

public record CreateMachineriesGroupCommand(
    string GroupName,
    string GroupCode,
    bool IsActive,
    long? CompanyId
    ) : ICommand<MachineriesGroup?>;