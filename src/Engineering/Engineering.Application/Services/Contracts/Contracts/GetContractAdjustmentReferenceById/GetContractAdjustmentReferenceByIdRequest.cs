namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferenceById;

public record GetContractAdjustmentReferenceByIdRequest(
    long Id) : IHttpRequest;
