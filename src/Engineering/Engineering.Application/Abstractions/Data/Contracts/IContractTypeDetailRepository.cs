using Engineering.Application.Services.Contracts.Contracts.GetAvailableContractTypeDetailSources;
using Engineering.Application.Services.Contracts.Contracts.GetContractChangeAvailableItems;
using Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetailById;
using Engineering.Application.Services.Contracts.Models.ContractChanges;
using Engineering.Application.Services.Contracts.Models.ContractTypeDetails;
using Engineering.Domain.Entities.Contracts;

using ContractTypeDetailEntity = Engineering.Domain.Entities.Contracts.ContractTypeDetail;
using ContractTypeEntity = Engineering.Domain.Entities.Contracts.ContractType;

namespace Engineering.Application.Abstractions.Data.Contracts;

public interface IContractTypeDetailRepository : IBaseRepository<ContractTypeDetailEntity>
{
    Task<GetContractTypeDetailByIdResponse?> GetContractTypeDetailById(
        long contractId,
        long contractTypeId,
        long id,
        long companyId,
        CT ct);

    Task<ContractTypeDetailSourceModel?> GetProcurementContractTypeDetailSource(
        long sourceId,
        long projectId,
        long? excludedContractTypeDetailId,
        long companyId,
        CT ct);

    Task<ContractTypeDetailSourceModel?> GetConstructionContractTypeDetailSource(
        long sourceId,
        long projectId,
        long contractPartyId,
        long? excludedContractTypeDetailId,
        long companyId,
        CT ct);

    Task<ContractTypeDetailSourceModel?> GetServiceContractTypeDetailSource(
        long sourceId,
        long projectId,
        long? excludedContractTypeDetailId,
        long companyId,
        CT ct);

    Task<GetAvailableContractTypeDetailSourcesResponse?> GetAvailableContractTypeDetailSources(
        long contractId,
        long contractTypeId,
        long? projectOperationId,
        string? filterData,
        long companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<ContractChangeDetailResolutionContextModel>> GetContractChangeDetailResolutionContexts(
        long contractId,
        long? excludedContractChangeId,
        IReadOnlyCollection<long> contractTypeDetailIds,
        long companyId,
        CT ct);

    Task<List<long>> GetBaselineConstructionSourceIds(
        long contractId,
        IReadOnlyCollection<long> projectOperationDetailIds,
        long companyId,
        CT ct);

    ContractTypeDetailEntity? GetContractTypeDetailForMutation(
        ContractTypeEntity contractType,
        long detailId);

    bool HasActiveContractTypeDetailSource(
        ContractTypeEntity contractType,
        long sourceId);
}