namespace Engineering.Application.Services.Contracts.Contracts.GetContractFinancialInformation;

public record GetContractFinancialInformationRequest(
    long ContractId) : IHttpRequest;