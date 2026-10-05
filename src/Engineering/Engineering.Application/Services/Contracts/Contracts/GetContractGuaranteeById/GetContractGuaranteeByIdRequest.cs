namespace Engineering.Application.Services.Contracts.Contracts.GetContractGuaranteeById;

public record GetContractGuaranteeByIdRequest(long ContractId, long Id) : IHttpRequest;
