namespace Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetails;

public record GetContractTypeDetailsRequest(
    long ContractId,
    long ContractTypeId) : IHttpRequest;