namespace Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetFilteredGoodsManagerAssignments;

public record GetFilteredGoodsManagerAssignmentsRequest(
    List<long>? Ids,
    long? OrganizationId,
    long? ProductId,
    string? FilterData,
    int PageIndex,
    int PageSize) : IHttpRequest;