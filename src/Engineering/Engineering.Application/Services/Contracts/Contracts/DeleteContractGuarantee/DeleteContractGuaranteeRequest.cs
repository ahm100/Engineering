namespace Engineering.Application.Services.Contracts.Contracts.DeleteContractGuarantee;

public record DeleteContractGuaranteeRequest(long ContractId, long Id) : IHttpRequest;
