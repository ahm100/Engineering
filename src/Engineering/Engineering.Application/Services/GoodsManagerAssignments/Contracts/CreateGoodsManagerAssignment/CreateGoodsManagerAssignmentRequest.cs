namespace Engineering.Application.Services.GoodsManagerAssignments.Contracts.CreateGoodsManagerAssignment;


public record CreateGoodsManagerAssignmentRequest(
    long OrganizationId,
    List<long> ProductIds) : IHttpRequest;