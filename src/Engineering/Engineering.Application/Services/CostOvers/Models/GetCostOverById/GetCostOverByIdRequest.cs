namespace Engineering.Application.Services.CostOvers.Models.GetCostOverById;

public record GetCostOverByIdRequest(
    long Id)
    : IHttpRequest;