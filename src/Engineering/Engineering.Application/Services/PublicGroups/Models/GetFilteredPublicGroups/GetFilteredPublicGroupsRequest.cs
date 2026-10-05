namespace Engineering.Application.Services.PublicGroups.Models.GetFilteredPublicGroups;

public record GetFilteredPublicGroupsRequest(
    List<long>? ProductGroupIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
