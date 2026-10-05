namespace Engineering.Application.Services.Contracts.Contracts.CreateContractAdjustmentIndex;

public record CreateContractAdjustmentIndexRequest(
    long ReferenceId,
    string Code,
    string FaTitle,
    string EnTitle,
    string? Description) : IHttpRequest;
