using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Abstractions.Data.ContractorContracts;

public interface IContractorContractDetailRepository : IBaseRepository<ContractorContractDetail>
{
    Task<ContractorContractDetail?> GetContractorContractDetailById(
        long id,
        CT ct);

    Task<(List<ContractorContractDetail> Data, int RowCount)> GetFilteredContractorContractDetail(
        long? projectOperationServiceId,
        long? projectOperationId,
        DateTime? startDate,
        DateTime? endDate,
        long? contractorId,
        string? FilterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ContractorContractDetail> Data, int RowCount)> GetsFilteredContractorContractDetailReports(
       List<long>? ids,
       long? contractorContractId,
       long? contractorId,
       DateTime? fromDate,
       DateTime? toDate,
       long? companyId,
       string? filterData,
       string[]? orderBy,
       int pageIndex,
       int pageSize,
       CT ct);

    Task<(List<ContractorContractDetail> Data, int RowCount)> GetsContractorContractDetailByIds(
       List<long> ids,
       CT ct);
}
