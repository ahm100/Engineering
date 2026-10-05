namespace Engineering.Application.Services.Contracts.Contracts.DeleteContractType;

public record DeleteContractTypeRequest(
    long ContractId,
    long Id) : IHttpRequest;