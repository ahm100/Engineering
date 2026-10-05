namespace Engineering.Application.Services.MachineriesGroups.Models.UpdateMachineriesGroup;

public record UpdateMachineriesGroupResponse(
    long Id,
    string GroupName,
    string GroupCode,
    bool IsActive);
