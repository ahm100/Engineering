namespace Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupModels;

public record GetMachineriesGroupsModel(
    long Id,
    string GroupName,
    string GroupCode,
    bool IsActive
    );
