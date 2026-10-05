using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsServiceByProjectOperationDetailIds;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetAssignable;
using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetFilteredProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailDocument;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReporting;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Abstractions.Data.ProjectOperationDetails;

public interface IProjectOperationDetailRepository : IBaseRepository<ProjectOperationDetail>
{
    Task<ProjectOperationDetail?> FindByIdAndMore(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailByIdLessInclude(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailByIdForTotal(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailByIdForDaily(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailByIdWithoutInclude(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailWithVolumes(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailVolumes(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailWithStandards(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailWithExpertValues(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailWithProductVolumes(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetsProjectOperationDetailWithProductSupply(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailWithMachineryVolumes(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailDoneVolume(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetByIdForDelete(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetByIdWithDaily(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailForDelete(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailByIdLessIncludes(
        long id,
        CT ct);


    Task<ProjectOperationDetail?> GetProjectOperationDetailForContractorServices(
        long id,
        CT ct);

    Task<bool> GetProjectOperationDetailValidator(
        long projectOperationId,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailByIdNoIncluding(
        long id,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailByCode(
        string code,
        long? operationLocationId,
        long? companyId,
        CT ct);

    Task<string> CodeCreator(
        long? projectOperationId,
        long? operationLocationId,
        long? companyId,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailForValidates(
        long projectOperationId,
        long operationLocationId,
        string code,
        long? CompanyId,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailsByIdAsync(
        long projectOperationDetailId,
        CT ct);

    Task<ProjectOperationDetail?> GetProjectOperationDetailByContractor(
        long projectId,
        long contractorId,
        CT ct);

    Task<(List<ProjectOperationDetailsModel> Data, int RowCount)> GetsByProjectOperationId(
        List<long>? ids,
        long projectOperationId,
        string? privateName,
        string? privateCode,
        string? filterData,
        long? employerId,
        ProjectOperationDetailStatus? status,
        List<long>? contractorIds,
        DateTime? createDate,
        DateTime? startDate,
        DateTime? endDate,
        List<long>? serviceInfoIds,
        List<long>? implementationAssistantIds,
        List<long>? TechnicalAssistantIds,
        long? creatorId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsIncludeLessDetailByProjectOperationId(
        long projectOperationId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsByProjectOperationIdForVolumes(
        long projectOperationId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsByProjectOperationIdInEmployerContract(
        long projectOperationId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsProjectOperationDetailReportingModel> Data, int RowCount)> GetsProjectOperationDetailReporting(
        List<long>? ids,
        DateTime? startDate,
        DateTime? endDate,
        DateTime? createFrom,
        DateTime? createTo,
        long? costCenterId,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? projectOperationIds,
        List<long>? contractorIds,
        List<ProjectOperationDetailStatus>? statuses,
        string? locationFilterData,
        string? descriptionFilterData,
        string? dailyDescription,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsProjectOperationDetailByContractorIds(
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? projectOperationIds,
        List<long>? contractorIds,
        string? filterData,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsProjectOperationDetailForScheduling(
        long operationInfoId,
        long operationLocationId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsByExpertId(
        long projectOperationId,
        long expertId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsByMachineryId(
        long projectOperationId,
        long machineryId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsByProductId(
        long projectOperationId,
        long productId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetDailyProjectOperationDetailFilter(
        long projectOperationId,
        ProjectOperationDetailStatus? status,
        DateTime? startDate,
        DateTime? endDate,
        DateTime? createDate,
        List<long>? operationLocationIds,
        List<long>? serviceInfoIds,
        long? contractorId,
        string? FilterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsByIds(
        List<long> projectOperationIds,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsByIdsIncludeless(
        List<long> projectOperationIds,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsForVolumesByIds(
        List<long>? projectOperationDetailIds,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsForVolumesByProjectOperationId(
        long? projectOperationId,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsSummarizedByProjectOperationIds(
        List<long> projectOperationIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsMinimalByProjectOperationIds(
        List<long>? projectOperationIds,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsMinimalByProjectOperationDetialIds(
        List<long>? ids,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsProjectOperationDetailByIds(
        List<long>? Ids,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsProjectOperationDetailData(
        long projectOperationId,
        VolumeProductType type,
        long productGroupId,
        long? projectOperationDetailId,
        string? filterData,
        long? contractorId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetProjectOperationDetailsByRequestIdAsync(
        long projectOperationId,
        long productGroupId,
        long? projectOperationDetailId,
        string? filterData,
        long? contractorId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationLocation> Data, int RowCount)> GetOperationLocationForSchecdulingAsync(
        long costCenterId,
        long projectId,
        string? filterData,
        List<long>? operationInfoIds,
        List<long>? operationLocationIds,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationInfo> Data, int RowCount)> GetOperationInfoForSchecdulingAsync(
        long costCenterId,
        long projectId,
        string? filterData,
        List<long>? operationLocationIds,
        List<long>? operationInfoIds,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<ProjectOperationDetail>> GetForSchecdulingAsync(
        long costCenterId,
        long projectId,
        long projectOperationId,
        CT ct);

    Task<List<ProjectOperationDetail>> ValidatesProjectOperationDetailForScheduling(
        List<long> operationInfoIds,
        List<long> operationLocationIds,
        CT ct);

    Task<(List<long> Data, int RowCount)> GetsFilteredContractors(
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsContractorProjectOperationDetailReports(
        List<long>? ids,
        long? contractorId,
        long? costCenterId,
        List<long>? projectIds,
        List<long>? contractorContractIds,
        DateTime? fromDate,
        DateTime? toDate,
        long? companyId,
        string? filterData,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsContractorProjectOperationDetailDetailReports(
        List<long>? ids,
        long? contractorContractId,
        long? contractorId,
        DateTime? fromDate,
        DateTime? toDate,
        long? companyId,
        string? filterData,
        int pageIndex,
        int pageSize, CT ct);

    Task<GetsProjectOperationDetailDocumentResponse?> GetsProjectOperationDetailDocument(
        long projectOperationDetailId,
        CT ct);

    Task<List<ProjectOperationDetail>> GetTotalsByProjectOperationId(
        long projectOperationId,
        CT ct);

    Task<List<ProjectOperationDetail>> GetProjectOperationDetailByCategoryId(
        long projectOperationId,
        long categoryId,
        long? projectOperationDetailId,
        string? filterData,
        long? contractorId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<ProjectOperationDetail>> GetWithoutIncludeByIds(
        List<long> Ids,
        CT ct);

    Task<(List<GetsServiceByProjectOperationDetailIdsModel> Data, int RowCount)> GetsServiceByProjectOperationDetailIds(
        List<long> projectOperationDetailIds,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetFilteredProjectOperationDetailsOperationModel> Data, int RowCount)> GetProjectOperationDetailByCostCenterId(
    long costCenterId,
    List<long>? projectIds,
    List<long>? CategoryIds,
    List<long>? BranchIds,
    List<long>? SeasonIds,
    List<long>? OperationInfoIds,
    string? filterData,
    int pageIndex,
    int pageSize,
    CT ct);

    Task<(List<AssignablePODModel> Data, int RowCount)> GetsAssignableOperationBasedDetailsByProjectOperationId(
        long projectOperationId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<ProjectOperationDetail>> GetsForOperationBasedAssignmentByIds(
        long projectOperationId,
        List<long> projectOperationDetailIds,
        CT ct);
}