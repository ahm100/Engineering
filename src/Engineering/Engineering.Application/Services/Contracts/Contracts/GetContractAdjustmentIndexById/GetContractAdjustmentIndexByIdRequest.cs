namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentIndexById;

public record GetContractAdjustmentIndexByIdRequest(
    long ReferenceId,
    long Id) : IHttpRequest;
