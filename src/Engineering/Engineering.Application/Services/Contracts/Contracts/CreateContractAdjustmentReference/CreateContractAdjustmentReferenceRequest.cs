namespace Engineering.Application.Services.Contracts.Contracts.CreateContractAdjustmentReference;

public record CreateContractAdjustmentReferenceRequest(
    string Code,
    string FaTitle,
    string EnTitle,
    string? Description) : IHttpRequest;
