using Engineering.Application.Services.ContractorContracts.Contracts.GetConfirmedCCDailyServices;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractServiceReport;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Abstractions.Data.ContractorContracts;

public interface IContractorContractDetailServiceRepository : IBaseRepository<ContractorContractDetailService>
{
    Task<ContractorContractDetailService?> GetContractorContractDetailServiceById(
        long id,
        CT ct);

    Task<(List<GetsContractorContractServiceReportModel> Data, int RowCount)> GetsContractorContractServiceReport(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? contractorIds,
        List<long>? serviceInfoIds,
        List<long>? measurUnitIds,
        ContractorContractType? contractTypeId,
        DateTime? startDate,
        DateTime? endDate,
        DateTime? fromDate,
        DateTime? toDate,
        DateTime? fromCreated,
        DateTime? toCreated,
        ProjectOperationDetailStatus? status,
        ContractorContractStatus? contractStatus,
        string? filterData,
        string? filterDescription,
        string? filterServiceInfo,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetConfirmedCCDailyServicesModel> Data, int RowCount)> GetConfirmedCCDailyServices(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? contractorIds,
        List<long>? serviceInfoIds,
        List<long>? measurUnitIds,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        string? filterServiceInfo,
        string[]? orderBy,
        CT ct);

}
