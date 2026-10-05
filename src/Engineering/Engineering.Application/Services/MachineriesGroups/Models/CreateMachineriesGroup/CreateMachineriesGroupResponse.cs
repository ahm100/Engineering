namespace Engineering.Application.Services.MachineriesGroups.Models.CreateMachineriesGroup;

public record CreateMachineriesGroupResponse(
    long Id,
    string GroupCode,
    string GroupName,
    bool IsActive
    );
