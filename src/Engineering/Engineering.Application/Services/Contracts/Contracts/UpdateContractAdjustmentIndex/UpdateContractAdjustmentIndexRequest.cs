namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractAdjustmentIndex;

public record UpdateContractAdjustmentIndexRequest(
    long ReferenceId,
    long Id,
    string Code,
    string FaTitle,
    string EnTitle,
    string? Description) : IHttpRequest;
