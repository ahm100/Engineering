namespace Engineering.Application.Services.Contracts.Contracts.DeleteContractChange;

public record DeleteContractChangeRequest(long ContractId, long Id) : IHttpRequest;
