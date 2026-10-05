using Engineering.Application.Services.DailyProjectOperations.Models.GetDetailedDailyOperationTotals;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyOperationCreatedByProjectReport;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationDocument;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperation;
using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Abstractions.Data.DailyProjectOperations;

public interface IDailyProjectOperationRepository : IBaseRepository<DailyProjectOperation>
{
    Task<DailyProjectOperation?> GetByIdAsync(
        long dailyProjectOperationId, CT ct);

    Task<DailyProjectOperation?> GetDailyProjectOperationByLegacyId(
        long legacyId, CT ct);

    Task<DailyProjectOperation?> GetForDelete(
        long dailyProjectOperationId, CT ct);

    Task<(List<DailyProjectOperation> Data, int RowCount)> GetFilteredDailyProjectOperation(
        long dailyProjectOperationId,
        long? contractorId,
        decimal? length,
        decimal? width,
        decimal? height,
        decimal? weight,
        decimal? number,
        DateTime? startDate,
        DateTime? endDate,
        long? creatorId,
        string? FilterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsDetailedDailyProjectOperationModel> Data, int RowCount)> GetsDetailedDailyProjectOperation(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? contractorIds,
        List<long>? serviceInfoIds,
        List<long>? creatorIds,
        DateTime? startDate,
        DateTime? endDate,
        DateTime? fromDate,
        DateTime? toDate,
        List<ProjectOperationDetailStatus>? projectOperationDetailStatus,
        List<ProjectOperationDetailStatus>? status,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<GetDetailedDailyProjectOperationTotalsModel>> GetDetailedDailyProjectOperationTotals(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? contractorIds,
        List<long>? serviceInfoIds,
        List<long>? creatorIds,
        DateTime? startDate,
        DateTime? endDate,
        DateTime? fromDate,
        DateTime? toDate,
        List<ProjectOperationDetailStatus>? status,
        List<ProjectOperationDetailStatus>? projectOperationDetailStatus,
        string? filterData,
        CT ct);

    Task<GetsDailyProjectOperationDocumentResponse?> GetsDailyProjectOperationDocument(
        long dailyProjectOperationId, CT ct);

    Task<(List<long> Data, int RowCount)> GetsDailyCreator(CT ct);

    Task<(List<DailyProjectOperation> Data, int RowCount)> GetFilteredDailyProjectOperationForExcel(
        List<long>? ids,
        long dailyProjectOperationId,
        DateTime? startDate,
        DateTime? endDate,
        long? creatorId,
        string? FilterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<DailyProjectOperation>> GetDailyProjectOperationsByProjectOperationIds(
        List<long> projectOperationIds, CT ct);

    Task<List<DailyProjectOperation>> GetEmployerStatusStatementLimitDate(
        long projectId,
        long employerContractId, CT ct);

    Task<List<DailyProjectOperation>> GetTotalsByProjectOperationDetailId(
        long projectOperationDetailId, CT ct);

    Task<List<DailyProjectOperation>> GetOperationTotals(
        long projectOperationId,
        long? projectOperationDetailId, CT ct);

    Task<(List<long> Data, int RowCount)> GetsFilteredContractors(
    List<long>? costCenterIds,
    List<long>? projectIds,
    List<long>? operationInfoIds,
    List<long>? projectOperationIds,
    List<long>? projectOperationDetailIds,
    CT ct);

    Task<int> GetsDailyCreatedCount(
        CT ct);

    Task<List<GetsDailyOperationCreatedByProjectReportModel>> GetsDailyOperationCreatedByProjectReport(
        CT ct);
}
