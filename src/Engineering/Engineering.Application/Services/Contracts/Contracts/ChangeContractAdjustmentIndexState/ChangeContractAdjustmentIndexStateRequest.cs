namespace Engineering.Application.Services.Contracts.Contracts.ChangeContractAdjustmentIndexState;

public record ChangeContractAdjustmentIndexStateRequest(
    long ReferenceId,
    long Id,
    bool IsActive) : IHttpRequest;
