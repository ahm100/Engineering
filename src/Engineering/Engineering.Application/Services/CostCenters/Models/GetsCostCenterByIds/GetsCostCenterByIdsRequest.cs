namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterByIds;

public record GetsCostCenterByIdsRequest(
    List<long>? Ids,
    List<Guid>? PreferentialReferenceCodes,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
