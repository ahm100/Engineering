using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailCostOver;
using Engineering.Application.Services.ContractorContracts.Queries.GetsDraftableContractorCostOver;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Abstractions.Data.ContractorContracts;

public interface IContractorContractDetailCostOverRepository : IBaseRepository<ContractorContractDetailCostOver>
{
    Task<ContractorContractDetailCostOver?> GetContractorContractDetailCostOverById(
        long id,
        CT ct);

    Task<(List<GetsContractorContractDetailCostOverModel> Data, int RowCount)> GetsContractorContractDetailCostOver(
        long contractorContractHedearId,
        DateTime? stratDate,
        DateTime? endDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsDraftableContractorCostOverModel> Data, int RowCount)> GetsDraftableContractorCostOver(
        long contractorId,
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ContractorContractDetailCostOver> Data, int RowCount)> GetsContractorContractCostOverForCSS(
        long contractorId,
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct);

}
