namespace Engineering.Application.Services.CostOvers.Models.GetsCostOverByNameOrCode;

public record GetsCostOverByNameOrCodeRequest(
    string FilterData,
    int PageIndex,
    int PageSize)
    : IHttpRequest;