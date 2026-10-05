namespace Engineering.Application.Services.GoodsManagerAssignments.Contracts.UpdateGoodsManagerAssignment;

public record UpdateGoodsManagerAssignmentRequest(
    long OrganizationId,
    List<long>? CreateProductIds,
    List<long>? DeleteProductIds
    ) : IHttpRequest;