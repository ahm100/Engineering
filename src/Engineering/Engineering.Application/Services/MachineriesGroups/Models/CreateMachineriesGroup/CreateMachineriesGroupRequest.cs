namespace Engineering.Application.Services.MachineriesGroups.Models.CreateMachineriesGroup;

public record CreateMachineriesGroupRequest(
    string GroupCode,
    string GroupName,
    bool IsActive
     ) : IHttpRequest;
