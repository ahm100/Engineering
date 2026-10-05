namespace Engineering.Application.Services.OperationInfoGroups.Models.GetActiveOperationInfoGroups;

public record GetActiveOperationInfoGroupsRequest(
    string? FilterData,
    string? Code,
    string? Name,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;