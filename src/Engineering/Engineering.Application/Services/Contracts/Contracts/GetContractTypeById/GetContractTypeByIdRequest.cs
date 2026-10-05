namespace Engineering.Application.Services.Contracts.Contracts.GetContractTypeById;

public record GetContractTypeByIdRequest(
    long ContractId,
    long Id) : IHttpRequest;