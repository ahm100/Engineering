using Engineering.Application.Services.ProjectOperationDependencies.Contracts.RescheduleProjectOperations;
using Engineering.Application.Services.ProjectOperations.Commands.ProjectOperationWorkloder;
using Engineering.Application.Services.ProjectOperations.Models.GetCriticalPO;
using Engineering.Application.Services.ProjectOperations.Models.GetFltrBasePricedPOs;
using Engineering.Application.Services.ProjectOperations.Models.GetFltrPOForReports;
using Engineering.Application.Services.ProjectOperations.Models.GetFltrProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.GetPODate;
using Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationProgress;
using Engineering.Application.Services.ProjectOperations.Models.GetsForEmployerStatusStatement;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationEmployerReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsTotalProjectOperationReporting;
using Engineering.Application.Services.ProjectOperations.Models.Models;
using Engineering.Application.Services.Projects.Models.GetProjectPOTimelines;
using Engineering.Application.Services.Projects.Models.GetProjectProgress;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Abstractions.Data.ProjectOperations;

public interface IProjectOperationRepository : IBaseRepository<ProjectOperation>
{
    Task<bool> GetProjectOperationForValidating(
        long operationInfoId,
        long projectId,
        long unitOfMeasurementId, CT ct);

    Task<List<ProjectOperation>?> GetProjectOperationForImport(
        List<long> operationInfoIds,
        List<long> projectIds, CT ct);

    Task<ProjectOperation?> GetProjectOperationByIdNoIncluding(
        long id,
        CT ct);

    Task<ProjectOperation?> GetProjectOperationDocuments(
        long id,
        CT ct);

    Task<ProjectOperation?> GetProjectOperationForUpdate(
        long id,
        CT ct);

    Task<ProjectOperation?> GetProjectOperationByIdOperationInfoInclude(
        long id,
        CT ct);

    Task<ProjectOperation?> GetProjectOperationForRequestGoodsSupply(
        long id,
        CT ct);

    Task<ProjectOperationWorkloderModel?> ProjectOperationWorkloder(
        long id,
        CT ct);

    Task<ProjectOperation?> GetProjectOperationByParams(
        long projectId,
        long operationInfoId,
        long? employerContractId,
        long measurementId,
        CT ct);

    Task<ProjectOperation?> GetByProjectOperationDetailId(
        long projectOperationDetailId,
        CT ct);

    Task<ProjectOperation?> GetProjectOperationByIdIncludelessNew(
        long projectOperationId,
        CT ct);

    Task<ProjectOperation?> GetProjectOperationRealtionInfo(
        long projectOperationId,
        CT ct);

    Task<ProjectOperation?> ProjectOperationWorkloadManagement(
        long projectOperationId, CT ct);

    Task<ProjectOperation?> GetProjectOperationByIdLessInclude(
        long projectOperationId, CT ct);

    Task<ProjectOperation?> GetByIdWithDependencies(
        long projectOperationId, CT ct);

    Task<ProjectOperation?> GetForValidate(
        long projectId,
        long operationInfoId,
        long? employerContractId,
        long unitOfMeasurementId,
        CT ct);

    Task<ProjectOperation?> FindForDelete(
        long id, CT ct);

    Task<ProjectOperation?> GetProjectOperationById(
        long projectOperationId, CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsProjectOperationByIdIncludeLess(
        List<long> projectOperationIds,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsByEmployerContract(
        long employerContractId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsByOperationInfo(
        long operationInfoId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsByOperationInfoIncludeLess(
        long operationInfoId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsProposedPrice(
        long operationInfoId,
        string? filterData,
        DateTime? startDate,
        DateTime? endDate,
        long? employerId,
        long? costCenterId,
        long? projectId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsForPricing(
        long employerId,
        long projectId,
        long costCenterId,
        string? contractCode,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsProjectOperationByProjectModel> Data, int RowCount)> GetsByProject(
        long projectId,
        long? categoryId,
        long? branchId,
        long? seasonId,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<string>> GetExistingOperationInfoCodesInProject(
    long projectId,
    List<string> operationInfoCodes,
    CT ct);

    Task<(List<GetsProjectOperationByProjectModel> Data, int RowCount)> GetsFilteredByProject(
        long projectId,
        long? categoryId,
        long? branchId,
        long? seasonId,
        List<long>? contractorIds,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsProjectOperationByProjectIds(
        List<long>? projectIds,
        List<long>? contractorIds,
        long? categoryId,
        long? branchId,
        long? seasonId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsFilteredByProjectIds(
        List<long>? projectIds,
        List<long>? notShowProjectOperationIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsPrioritizeProjectOperation(
        string? filterData,
        long? id,
        int priority,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsForDailyProjectOperations(
        long projectId,
        int? Priority,
        string? filterData,
        DateTime? startDate,
        DateTime? endDate,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsWithoutContract(
        List<long>? ids,
        long projectId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsByProjectOperationId(
        long projectId,
        int? Priority,
        long? seasonId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsSummarizedProjectOperation(
        long projectId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsProjectOperationDependency(
        long projectId,
        long operationInfo,
        CT ct);

    Task<(List<GetsProjectOperationReportingModel> Data, int RowCount)> GetsProjectOperationReporting(
        List<long>? ids,
        long? costCenterId,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? contractorIds,
        List<ProjectOperationStatus>? statuses,
        DateTime? startDate,
        DateTime? endDate,
        string? description,
        string? dailyDescription,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsTotalProjectOperationReportingResponseModel> Data, int RowCount)> GetsTotalProjectOperationReporting(
        List<long>? ids,
        long? costCenterId,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? contractorIds,
        List<ProjectOperationStatus>? statuses,
        DateTime? startDate,
        DateTime? endDate,
        string? description,
        string? dailyDescription,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsProjectOperationEmployerReportingModel> Data, int RowCount)> GetsProjectOperationEmployerReporting(
        List<long>? ids,
        long? costCenterId,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? contractorIds,
        List<long>? employerIds,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsProjectOperationDailyReportingModel> Data, int RowCount)> GetsProjectOperationDailyReporting(
        List<long>? ids,
        long? costCenterId,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsForEmployerStatusStatementModel> Data, int RowCount)> GetsForEmployerStatusStatement(
        long? employerId,
        long? employerStatusStatementId,
        long projectId,
        long costCenterId,
        long? employerContractId,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsProjectOperationByIds(
        List<long>? projectOperationIds,
        string? filterData,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsProjectOperationByIdsIncludeless(
        List<long>? projectOperationIds,
        string? filterData,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetFilteredProjectOperations(
        List<long> projectOperationIds,
        CT ct);

    Task<List<ProjectOperation>> GetProjectOperations(
        List<long> ids,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetProjectOperationByIdsLessInclude(
        List<long> projectOperationIds,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetDailyProjectOperationFilter(
        long costCenterId,
        long projectId,
        long? contractorId,
        DateTime? startDate,
        DateTime? endDate,
        string? FilterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetFilteredProjectOperationsByProjectIds(
        long costCenterId,
        List<long> projectIds,
        long? contractorId,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<ProjectOperation>> GetForSchecdulingAsync(
        long projectId,
        long operationInfo,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsWithoutContractProjectOperationForESS(
        long projectId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperation> Data, int RowCount)> GetsWithContractProjectOperationForESS(
        long projectId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<ProjectOperation>> GetWithoutIncludeByIds(
        List<long> Ids,
        CT ct);

    Task<List<ProjectOperation>> GetByProjectCodesAndOInfoCodes(
        List<string> projectCodes,
        List<string> oInfoCodes,
        CT ct);

    Task<ProjectOperation?> FindMergedProjectOperation(
        long projectOperationId,
        long projectId,
        long unitOfMeasurementId,
        long operationInfoId,
        CT ct);

    Task<List<GetFltrBasePricedPOsModel>> GetFltrBasePricedPOs(
        long costCenterId,
        List<long>? projectIds,
        List<long>? actionIds,
        List<long>? categoryIds,
        List<long>? branchIds,
        List<long>? seasonIds,
        List<long>? operationInfoIds,
        string? filterData,
        CT ct);

    Task<List<GetFltrProjectOperationModel>> GetFltrProjectOperation(
       long costCenterId,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? measureUnitIds,
        decimal? minPrice,
        decimal? maxPrice,
        string? filterData,
        int pageIndex,
        int pageSize,
       CT ct);

    Task<List<GetFltrPOForReportsModel>> GetFltrPOForReports(
    List<long>? costCenterIds,
    List<long>? projectIds,
    string? filterData,
    int pageIndex,
    int pageSize,
    CT ct);

    Task<GetProjectOperationProgressResponse?> GetProjectOperationProgress(
        long id, CT ct);

    Task<List<ProjectOperation>> GetByIds(
        List<long> ids,
        CT ct);

    Task<GetPODateResponse?> GetPODate(
        long id, CT ct);

    Task<List<ProjectOperation>?> GetByProjectId(
        long id, CT ct);

    Task<int?> GetDelayed(
        long id, CT ct);

    Task<int?> GetCriticalPO(
        long id, CT ct);

    Task<DateTime?> GetLastPO(
    long id,
    CT ct);

    Task<DateTime?> GetFirstPO(
    long id,
    CT ct);

    Task<(List<GetCriticalPOModel>? Data, int RowCount)> GetCriticalPOModel(
        long id,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetProjectPOTimelinesModel>? Data, int Count)> GetProjectPOTimelines(
        long id, CT ct);

    Task<(List<GetProjectProgressModel>? Data, int Count)> GetProjectProgress(
    long id, CT ct);

    Task<List<ProjectOperationScheduleModel>> GetOperationsForReschedule(
    List<long> ids,
    CT ct);
}