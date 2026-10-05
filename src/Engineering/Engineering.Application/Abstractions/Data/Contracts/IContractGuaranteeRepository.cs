using Engineering.Application.Services.Contracts.Contracts.GetContractGuaranteeById;
using Engineering.Application.Services.Contracts.Contracts.GetContractGuarantees;
using Engineering.Domain.Entities.Contracts;

using ContractEntity = Engineering.Domain.Entities.Contracts.Contract;

namespace Engineering.Application.Abstractions.Data.Contracts;

public interface IContractGuaranteeRepository : IBaseRepository<ContractGuarantee>
{
    Task<GetContractGuaranteeByIdResponse?> GetContractGuaranteeById(
        long contractId,
        long guaranteeId,
        long companyId,
        CT ct);

    Task<List<GetContractGuaranteesModel>> GetContractGuarantees(
        long contractId,
        long companyId,
        CT ct);

    bool HasActiveGuarantee(
        ContractEntity contract,
        long guaranteeId);
}