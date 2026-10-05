using Engineering.Application.Services.ContractorContracts.Contracts.GetCContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsDraftableContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Abstractions.Data.ContractorContracts;

public interface IContractorContractHeaderRepository : IBaseRepository<ContractorContractHeader>
{
    Task<ContractorContractHeader?> GetContractorContractHeaderById(long id, CT ct);
    Task<GetCCHByIdResponse?> GetCCHById(
        long id, long companyId, CancellationToken ct);

    Task<GetContractorContractHeaderByIdResponse?> GetContractorContractHeaderByIdNew(long id, long companyId, CT ct);

    Task<ContractorContractHeader?> GetHeaderByIdForContractorStatusStatement(long id, CT ct);

    Task<ContractorContractHeader?> GetContractorContractHeaderByIdIncludeless(long id, CT ct);
    Task<ContractorContractHeader?> GetContractorContractHeaderForDelete(long id, CT ct);

    Task<(List<GetsFilteredContractorContractHeaderModel> Data, int RowCount)> GetContractorContractHeaderByFilter(
        List<long>? ids,
        long? contractorId,
        long? costCenterId,
        List<long>? projectIds,
        long? projectManagerId,
        List<long>? projectOperationIds,
        DateTime? fromDate,
        DateTime? toDate,
        List<ContractorContractStatus>? statuses,
        List<ContractorContractStatus>? removeStatuses,
        ContractorContractType? contractorContractTypeId,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsDraftableContractorContractHeaderModel> Data, int RowCount)> GetsDraftableContractorContractHeader(
        long contractorId,
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ContractorContractHeader> Data, int RowCount)> GetsContractorContractHeaderByIds(
        List<long> ids,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<ContractorContractHeader> Data, int RowCount)> GetsContractorContractHeader(long contractorId, long projectId, int pageIndex, int pageSize, CT ct);

    Task<(List<ContractorContractHeader> Data, int RowCount)> GetsHeaderForContractorStatusStatement(long contractorId, long projectId, int pageIndex, int pageSize, CT ct);
}
