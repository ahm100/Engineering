using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentConfiguration;
using Engineering.Domain.Entities.Contracts;

namespace Engineering.Application.Abstractions.Data.Contracts;

public interface IContractAdjustmentConfigurationRepository : IBaseRepository<ContractAdjustmentConfiguration>
{
    Task<GetContractAdjustmentConfigurationResponse?> GetContractAdjustmentConfiguration(
        long contractId,
        long companyId,
        CT ct);
}