namespace Engineering.Application.Services.Contracts.Contracts.GetContractChangeById;

public record GetContractChangeByIdRequest(long ContractId, long Id) : IHttpRequest;
