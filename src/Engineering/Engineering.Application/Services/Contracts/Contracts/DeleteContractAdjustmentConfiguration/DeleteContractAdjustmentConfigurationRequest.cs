namespace Engineering.Application.Services.Contracts.Contracts.DeleteContractAdjustmentConfiguration;
public record DeleteContractAdjustmentConfigurationRequest(long ContractId, long Id) : IHttpRequest;
