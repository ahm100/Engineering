namespace Engineering.Application.Services.Tasks.Contracts.GetTaskGroups;

public record GetTaskGroupsRequest(
    long? ownerThirdPartyId,
    int? PageIndex,
    int? PageSize
) : IHttpRequest;
