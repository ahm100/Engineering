namespace Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroups;

public record GetMachineriesGroupsRequest(
    string? FilterData,
    string? Code,
    string? Name,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
