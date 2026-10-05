using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReport;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Abstractions.Data.ProjectOperationDetails;

public interface IProjectOperationDetailInspectionRepository : IBaseRepository<ProjectOperationDetailInspection>
{
    Task<ProjectOperationDetailInspection?> GetById(long id, CT ct);
    Task<(List<ProjectOperationDetailInspection> Data, int RowCount)> GetProjectOperationDetailInspections(
        long? costCenterId,
        long? projectId,
        long? operationInfo,
        long? operationLocation,
        long? projectOperationId,
        long? projectOperationDetailId,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);
    Task<(List<GetsInspectionReportModel> Data, int RowCount)> GetsInspectioReport(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? operationLocationIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? creatorIds,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<long> Data, int RowCount)> GetsInspectionCreator(CT ct);
}