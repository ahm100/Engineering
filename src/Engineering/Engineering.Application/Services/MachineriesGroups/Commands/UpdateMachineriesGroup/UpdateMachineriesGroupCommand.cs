using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Commands.UpdateMachineriesGroup;

public record UpdateMachineriesGroupCommand(
    long Id,
    string GroupName,
    string GroupCode,
    bool IsActive,
    long? CompanyId
    ) : ICommand<MachineriesGroup>;