namespace Engineering.Application.Services.GoodsManagerAssignments.Contracts.DeleteGoodsManagerAssignment;

public record DeleteGoodsManagerAssignmentRequest(
    long OrganizationId) : IHttpRequest;