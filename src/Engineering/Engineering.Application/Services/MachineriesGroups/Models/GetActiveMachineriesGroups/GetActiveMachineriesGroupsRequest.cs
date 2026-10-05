namespace Engineering.Application.Services.MachineriesGroups.Models.GetActiveMachineriesGroups;

public record GetActiveMachineriesGroupsRequest(
    string? FilterData,
    string? Code,
    string? Name,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
