namespace Engineering.Application.Services.ProjectOperations.Models.GetsForPricing;

public record GetsForPricingRequest(
    long EmployerId,
    long ProjectId,
    long CostCenterId,
    string? ContractCode,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
