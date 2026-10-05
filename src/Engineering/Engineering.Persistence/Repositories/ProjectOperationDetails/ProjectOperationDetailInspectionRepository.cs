using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReport;
using ProjectOperationDetailInspection = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailInspection;

namespace Engineering.Persistence.Repositories.ProjectOperationDetails;

public class ProjectOperationDetailInspectionRepository : BaseRepository<EngineeringDBContext, ProjectOperationDetailInspection>, IProjectOperationDetailInspectionRepository
{
    public ProjectOperationDetailInspectionRepository(EngineeringDBContext context) : base(context)
    {
    }

#pragma warning disable CS8602 // Dereference of a possibly null reference.

    public async Task<ProjectOperationDetailInspection?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.ProjectOperationDetailInspectionDocuments)
            .Include(oo => oo.ProjectOperation)
                .ThenInclude(x => x.OperationInfo)
            .Include(oo => oo.ProjectOperationDetail)
                .ThenInclude(x => x.OperationLocation)
            .Include(oo => oo.OperationLocation)
            .Include(oo => oo.OperationInfo)
            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<ProjectOperationDetailInspection> Data, int RowCount)> GetProjectOperationDetailInspections(
        long? costCenterId, long? projectId, long? operationInfo, long? operationLocation, long? projectOperationId,
        long? projectOperationDetailId, DateTime? fromDate, DateTime? toDate, string? filterData, string[]? orderBy,
        int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
             .Include(oo => oo.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.ProjectOperationDetailInspectionDocuments)
            .Include(oo => oo.ProjectOperation)
                .ThenInclude(x => x.OperationInfo)
            .Include(oo => oo.ProjectOperationDetail)
                .ThenInclude(x => x.OperationLocation)
            .Include(oo => oo.OperationLocation)
            .Include(oo => oo.OperationInfo)
            .Where(oo =>
            (costCenterId == null || oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
            (projectId == null || oo.Project.Id == projectId) &&
            (operationInfo == null || oo.OperationInfo.Id == operationInfo) &&
            (operationLocation == null || oo.OperationLocation.Id == operationLocation) &&
            (projectOperationId == null || oo.ProjectOperation.Id == projectOperationId) &&
            (projectOperationDetailId == null || oo.ProjectOperationDetail.Id == projectOperationDetailId) &&
            (fromDate == null || (oo.InspectionDate.HasValue && oo.InspectionDate.Value.Date >= fromDate.Value.Date)) &&
            (toDate == null || (oo.InspectionDate.HasValue && oo.InspectionDate.Value.Date <= toDate.Value.Date)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.Description, filterData.MakeLikePattern())) &&
            oo.IsDeleted == false);

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsInspectionReportModel> Data, int RowCount)> GetsInspectioReport(
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
        CT ct)
    {
        var query = DbSet
            .Where(i =>
                (ids == null || ids.Contains(i.Id)) &&
                (creatorIds == null || creatorIds.Contains(i.CreatorId)) &&
                (costCenterIds == null || i.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (projectIds == null || projectIds.Contains(i.Project.Id)) &&
                (operationInfoIds == null || operationInfoIds.Contains(i.OperationInfo.Id)) &&
                (operationLocationIds == null || operationLocationIds.Contains(i.OperationLocation.Id)) &&
                (projectOperationIds == null || projectOperationIds.Contains(i.ProjectOperation.Id)) &&
                (projectOperationDetailIds == null || projectOperationDetailIds.Contains(i.ProjectOperationDetail.Id)) &&
                (fromDate == null || (i.InspectionDate.HasValue && i.InspectionDate.Value.Date >= fromDate.Value.Date)) &&
                (toDate == null || (i.InspectionDate.HasValue && i.InspectionDate.Value.Date <= toDate.Value.Date)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                 EF.Functions.Like(i.Description, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(i.ProjectOperationDetail.Description, filterData.MakeLikePattern())) &&
                i.IsDeleted == false)
            .Select(i => new GetsInspectionReportModel()
            {
                CreatorId = i.CreatorId,
                CostCenterName = i.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
                CostCenterCode = i.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterCode,
                CostCenterId = i.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,
                ProjectName = i.Project.ProjectName,
                ProjectCode = i.Project.ProjectCode,
                ProjectId = i.Project.Id,
                CreateDate = i.Created,
                Description = i.Description,
                DetailHeight = i.ProjectOperationDetail.Height,
                DetailLength = i.ProjectOperationDetail.Length,
                DetailNumber = i.ProjectOperationDetail.Number,
                DetailWidth = i.ProjectOperationDetail.Width,
                DetailWeight = i.ProjectOperationDetail.Weight,
                Length = i.Length,
                Width = i.Width,
                Weight = i.Weight,
                Height = i.Height,
                Number = i.Number,
                HaveDocument = i.ProjectOperationDetailInspectionDocuments != null && i.ProjectOperationDetailInspectionDocuments.Count > 0 ? true : false,
                Id = i.Id,
                InspectionDate = i.InspectionDate,
                OperationInfoCode = i.ProjectOperation.OperationInfo.OperationInfoCode,
                OperationInfoName = i.ProjectOperation.OperationInfo.OperationInfoName,
                OperationInfoId = i.ProjectOperation.OperationInfo.Id,
                OperationLocationId = i.ProjectOperationDetail.OperationLocation.Id,
                PrivateCode = i.ProjectOperationDetail.OperationLocation.PrivateCode,
                PrivateName = i.ProjectOperationDetail.OperationLocation.PrivateName,
                PublicCode = i.ProjectOperationDetail.OperationLocation.PublicCode,
                PublicName = i.ProjectOperationDetail.OperationLocation.PublicName,
                ProjectOperationDetailId = i.ProjectOperationDetail.Id,
                ProjectOperationId = i.ProjectOperation.Id,
                Workload = i.ProjectOperation.Workload,
                DetailDescription = i.ProjectOperationDetail.Description,
                ProjectOperationDetailCode = i.ProjectOperationDetail.Code,
                Documents = i.ProjectOperationDetailInspectionDocuments.Select(d => new InspectionDocumentResponseModel()
                {
                    Id = d.Id,
                    Url = d.Url
                }).ToList(),
                Deductions = i.ProjectOperationDetail.ProjectOperationDetailDeductions.Select(d => new InspectionDetailDeductionResponseModel
                {
                    Height = d.Height,
                    Length = d.Length,
                    Number = d.Number,
                    Weight = d.Weight,
                    Width = d.Width,
                    Id = d.Id
                }).ToList(),
            });

        query = query.OrderByDescending(oo => oo.CreateDate);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<long> Data, int RowCount)> GetsInspectionCreator(CT ct)
    {
        var query = DbSet
            .Where(x => !x.IsDeleted)
            .Select(c => c.CreatorId).Distinct();

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }
}