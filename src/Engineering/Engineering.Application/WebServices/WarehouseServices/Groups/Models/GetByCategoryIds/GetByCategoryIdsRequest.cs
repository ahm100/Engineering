namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetByCategoryIds;

public record GetGroupByCategoryIdsRequest(
    List<long> CategoryIds,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
