namespace Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetGoodsManagerAssignmentsById;

public record GetGoodsManagerAssignmentsByIdRequest(
    long Id) : IHttpRequest;