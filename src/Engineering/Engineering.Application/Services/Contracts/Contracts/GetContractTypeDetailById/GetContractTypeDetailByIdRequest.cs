namespace Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetailById;

public record GetContractTypeDetailByIdRequest(
    long ContractId,
    long ContractTypeId,
    long Id) : IHttpRequest;