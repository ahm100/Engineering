using Engineering.Application.Services.Contracts.Contracts.GetContractFinancialInformation;
using Engineering.Domain.Entities.Contracts;

namespace Engineering.Application.Abstractions.Data.Contracts;

public interface IContractFinancialInformationRepository : IBaseRepository<ContractFinancialInformation>
{
    Task<GetContractFinancialInformationResponse?> GetContractFinancialInformation(
        long contractId,
        long companyId,
        CT ct);
}