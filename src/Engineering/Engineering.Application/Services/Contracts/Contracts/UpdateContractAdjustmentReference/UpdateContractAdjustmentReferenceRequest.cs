namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractAdjustmentReference;

public record UpdateContractAdjustmentReferenceRequest(
    long Id,
    string Code,
    string FaTitle,
    string EnTitle,
    string? Description) : IHttpRequest;
