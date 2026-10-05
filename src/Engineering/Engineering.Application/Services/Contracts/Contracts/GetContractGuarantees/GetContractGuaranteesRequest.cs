namespace Engineering.Application.Services.Contracts.Contracts.GetContractGuarantees;

public record GetContractGuaranteesRequest(long ContractId) : IHttpRequest;
