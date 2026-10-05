using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationService;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyServiceInfo;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsTotalDailyProjectOperationService;
using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Abstractions.Data.DailyProjectOperations;

public interface IDailyProjectOperationServiceRepository : IBaseRepository<DailyProjectOperationService>
{

    Task<(List<GetsDailyProjectOperationServiceModel> Data, int RowCount)> GetsDailyProjectOperationService(
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
        DateTime? fromDate,
        DateTime? toDate,
        ProjectOperationDetailStatus? status,
        string? filterData,
        string? filterServiceInfo,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<decimal> GetsTotalPriceDailyContractorService(
        long projectId,
        long contractorId,
        DateTime? startDate,
        DateTime? endDate,
        CT ct);

    Task<(List<GetsDailyServiceInfoModel> Data, int RowCount)> GetsDailyServiceInfo(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? contractorIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<GetDraftedFixDailiesModel>> GetDraftedFixDailies(
        List<long> ids,
        CT ct);

    Task<List<GetDraftedServiceDailiesModel>> GetDraftedServiceDailies(
        List<long> ids,
        DateTime startDate,
        DateTime endDate,
        CT ct);

    Task<List<GetsTotalDailyProjectOperationServiceModel>> GetsTotalDailyProjectOperationService(
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
        DateTime? fromDate,
        DateTime? toDate,
        ProjectOperationDetailStatus? status,
        string? filterData,
        string? filterServiceInfo,
        CT ct);
}
