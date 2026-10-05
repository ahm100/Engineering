namespace Engineering.Application.Services.Contracts.Contracts.ChangeContractAdjustmentReferenceState;

public record ChangeContractAdjustmentReferenceStateRequest(
    long Id,
    bool IsActive) : IHttpRequest;
