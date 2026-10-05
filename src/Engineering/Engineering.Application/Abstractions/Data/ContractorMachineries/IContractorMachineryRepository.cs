using Engineering.Application.Services.ContractorMachineries.Models.GetFltrByContractorIds;
using Engineering.Domain.Entities.ContractorMachineries;
using Engineering.Domain.Entities.ContractorMachineries.Enums;

namespace Engineering.Application.Abstractions.Data.ContractorMachineries;

public interface IContractorMachineryRepository : IBaseRepository<ContractorMachinery>
{
    Task<ContractorMachinery?> GetById(long id, CT ct);

    Task<ContractorMachinery?> IsDuplicate(long contractorId, long MachineryId, ContractorMachineryUnit unit, long? companyId, CT ct);

    Task<ContractorMachinery?> GetContractorMachineryForDelete(long id, CT ct);

    Task<(List<ContractorMachinery> Data, int RowCount)> GetsContractorMachineryByIds(List<long> ids, int pageIndex, int pageSize, CT ct);

    Task<(List<ContractorMachinery> Data, int RowCount)> GetContractorMachineries(List<long>? ids, List<long>? machineryIds, List<long>? contractorIds,
        ContractorMachineryUnit? unit, DateTime? fromDate, DateTime? toDate, string? filterData, bool? isActive, long? companyId, string[]? orderBy,
        int pageIndex, int pageSize, CT ct);

    Task<(List<ContractorMachinery> Data, int RowCount)> GetsByContractorId(long contractorId, string? filterData, bool? isActive, long? companyId,
        string[]? orderBy, int pageIndex, int pageSize, CT ct);

    Task<(List<ContractorMachinery> Data, int RowCount)> GetActiveContractorMachineries(List<long>? machineryIds, List<long>? contractorIds,
        ContractorMachineryUnit? unit, DateTime? fromDate, DateTime? toDate, string? filterData, long? companyId, string[]? orderBy,
        int pageIndex, int pageSize, CT ct);

    Task<(List<GetFltrByContractorIdsModel> Data, int RowCount)> GetFltrByContractorIds(
            List<long> contractorIds,
            CT ct);
}