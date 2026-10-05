using Engineering.Application.Services.Contracts.Contracts.GetContractTypeById;
using Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetails;
using Engineering.Application.Services.Contracts.Models.ContractChanges;
using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.Contracts.Enums;

using ContractTypeEntity = Engineering.Domain.Entities.Contracts.ContractType;

namespace Engineering.Application.Abstractions.Data.Contracts;

public interface IContractTypeRepository : IBaseRepository<ContractTypeEntity>
{
    Task<GetContractTypeByIdResponse?> GetContractTypeById(
        long contractId,
        long contractTypeId,
        long companyId,
        CT ct);

    Task<GetContractTypeDetailsResponse?> GetContractTypeDetails(
        long contractId,
        long contractTypeId,
        long companyId,
        CT ct);
}