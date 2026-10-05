namespace Engineering.Application.Services.Contracts.Contracts.DeleteContractTypeDetail;

public record DeleteContractTypeDetailRequest(
    long ContractId,
    long ContractTypeId,
    long Id) : IHttpRequest;