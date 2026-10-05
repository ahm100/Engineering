namespace Engineering.Application.Services.MachineriesGroups.Models.UpdateMachineriesGroup;

public record UpdateMachineriesGroupRequest(
    long Id,
    string GroupName,
    string GroupCode,
    bool IsActive
     ) : IHttpRequest;
