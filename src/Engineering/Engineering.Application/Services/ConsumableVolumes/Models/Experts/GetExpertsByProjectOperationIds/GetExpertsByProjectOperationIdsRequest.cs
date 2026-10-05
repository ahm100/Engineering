namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetExpertsByProjectOperationIds;

public record GetExpertsByProjectOperationIdsRequest(
    List<long> ProjectOperationIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
