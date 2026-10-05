namespace Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetGoodsManagerAssignmentHistory;

public record GetGoodsManagerAssignmentHistoryRequest(
    long Id,
    int PageIndex,
    int PageSize) : IHttpRequest;