using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDetailedDailyOperationTotals;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyOperationCreatedByProjectReport;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationDocument;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperation;
using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Persistence.Repositories.DailyProjectOperations;

public partial class DailyProjectOperationRepository : BaseRepository<EngineeringDBContext, DailyProjectOperation>, IDailyProjectOperationRepository
{
    public DailyProjectOperationRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<DailyProjectOperation?> GetByIdAsync(
        long id, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(oo => oo.DailyProjectOperationDocuments)
            .Include(oo => oo.DailyProjectOperationExperts)
               .ThenInclude(oo => oo.ConsumableVolumeExpert)
            .Include(oo => oo.DailyProjectOperationMachineries)
               .ThenInclude(oo => oo.ConsumableVolumeMachinery)
            .Include(oo => oo.DailyProjectOperationMachineries)
               .ThenInclude(oo => oo.RequestMachinery)
                   .ThenInclude(oo => oo.Machinery)
                       .ThenInclude(oo => oo.MachineriesGroup)
            .Include(oo => oo.DailyProjectOperationRequestRewards)
               .ThenInclude(oo => oo.RequestReward)
            .Include(oo => oo.DailyProjectOperationProducts)
               .ThenInclude(oo => oo.ConsumableVolumeProduct)
            .Include(oo => oo.DailyProjectOperationServices)
               .ThenInclude(oo => oo.ProjectOperationDetailContractorService)
                   .ThenInclude(oo => oo.OperationInfoService.ServiceInfo)
            .Include(oo => oo.ProjectOperationDetail)
               .ThenInclude(oo => oo.ProjectOperation)
                   .ThenInclude(oo => oo.OperationInfo)
            .Include(oo => oo.ProjectOperationDetail)
               .ThenInclude(oo => oo.OperationLocation)
            .Include(oo => oo.ProjectOperationDetail)
               .ThenInclude(oo => oo.ProjectOperation)
                   .ThenInclude(oo => oo.Project)
            .Include(oo => oo.ProjectOperationDetail)
               .ThenInclude(oo => oo.ProjectOperation)
                   .ThenInclude(oo => oo.Project)
                       .ThenInclude(oo => oo.ProjectCostCenters)
                           .ThenInclude(oo => oo.CostCenter)

            .Where(oo => oo.Id.Equals(id));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var entity = await query.AsNoTracking().SingleOrDefaultAsync();
        return entity;
    }

    public async Task<DailyProjectOperation?> GetDailyProjectOperationByLegacyId(
        long legacyId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.LegacyId != null && oo.LegacyId.Equals(legacyId));

        var entity = await query.AsNoTracking().SingleOrDefaultAsync();
        return entity;
    }

    public async Task<DailyProjectOperation?> GetForDelete(
        long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.DailyProjectOperationServices)
                .ThenInclude(oo => oo.ContractorStatusStatementServiceDailies)
            .Include(oo => oo.ContractorStatusStatementServices)
                .ThenInclude(oo => oo.ContractorStatusStatementDetail)
                    .ThenInclude(oo => oo.ContractorStatusStatement)
            .Include(oo => oo.ProjectOperationDetail)
                    .ThenInclude(oo => oo.DailyOperations)
            .Include(oo => oo.ProjectOperationDetail)
                .ThenInclude(oo => oo.EmployerStatusStatementProjectOperationDetails)
                    .ThenInclude(oo => oo.EmployerStatusStatementProjectOperation)
                        .ThenInclude(oo => oo.EmployerStatusStatement)


            .Where(oo => oo.Id.Equals(id));

        var entity = await query.SingleOrDefaultAsync();
        return entity;
    }

    public async Task<List<DailyProjectOperation>> GetDailyProjectOperationsByProjectOperationIds(
        List<long> projectOperationIds, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet.Include(oo => oo.ProjectOperationDetail)
                            .ThenInclude(oo => oo.ProjectOperation)
                         .Include(oo => oo.DailyProjectOperationExperts)
                            .ThenInclude(oo => oo.ConsumableVolumeExpert)
                         .Include(oo => oo.DailyProjectOperationServices)
                            .ThenInclude(oo => oo.ProjectOperationDetailContractorService)
                                .ThenInclude(oo => oo.OperationInfoService.ServiceInfo)
                         .Include(oo => oo.DailyProjectOperationMachineries)
                            .ThenInclude(oo => oo.RequestMachinery)
                                .ThenInclude(oo => oo.Machinery)
                                    .ThenInclude(oo => oo.MachineriesGroup)
                         .Include(oo => oo.DailyProjectOperationMachineries)
                            .ThenInclude(oo => oo.ConsumableVolumeMachinery)

                         .Where(oo =>
                                     projectOperationIds.Contains(oo.ProjectOperationDetail.ProjectOperation.Id)
                                     )
            .OrderByDescending(oo => oo.Created);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return await query.ToListAsync(ct);
    }

    public async Task<List<DailyProjectOperation>> GetEmployerStatusStatementLimitDate(
        long projectId,
        long employerContractId, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Where(x =>
            x.ProjectOperationDetail.ProjectOperation.Project.Id == projectId &&
            ///TODO Employers
            //x.ProjectOperationDetail.ProjectOperation.EmployerContract.Id == employerContractId &&
            !x.IsDeleted);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return await query.ToListAsync(ct);
    }

    public async Task<List<DailyProjectOperation>> GetTotalsByProjectOperationDetailId(
        long projectOperationDetailId, CT ct)
    {
        var query = DbSet
                         .Include(x => x.ProjectOperationDetail)
                            .ThenInclude(x => x.ProjectOperation)
                         .Include(x => x.ProjectOperationDetail)
                            .ThenInclude(x => x.ProjectOperationDetailDeductions)

                         .Where(x => !x.IsDeleted &&
                                      x.ProjectOperationDetail.Id == projectOperationDetailId);

        return await query.ToListAsync(ct);
    }

    public async Task<List<DailyProjectOperation>> GetOperationTotals(
        long projectOperationId,
        long? projectOperationDetailId, CT ct)
    {
        var query = DbSet
                         .Include(x => x.ProjectOperationDetail)
                            .ThenInclude(x => x.ProjectOperation)
                         .Include(x => x.ProjectOperationDetail)
                            .ThenInclude(x => x.ProjectOperationDetailDeductions)

                         .Where(x => !x.IsDeleted &&
                                      x.ProjectOperationDetail.ProjectOperation.Id == projectOperationId &&
                                     (projectOperationDetailId == null || x.ProjectOperationDetail.Id == projectOperationDetailId));

        return await query.ToListAsync(ct);
    }

    public async Task<(List<DailyProjectOperation> Data, int RowCount)> GetFilteredDailyProjectOperation(
        long projectOperationDetailId,
        long? contractorId,
        decimal? length,
        decimal? width,
        decimal? height,
        decimal? weight,
        decimal? number,
        DateTime? startDate,
        DateTime? endDate,
        long? creatorId,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.ProjectOperationDetail)
               .ThenInclude(oo => oo.ProjectOperation)
            .Include(oo => oo.ProjectOperationDetail)
               .ThenInclude(oo => oo.ProjectOperationDetailDeductions)
            .Include(oo => oo.ProjectOperationDetail)
               .ThenInclude(oo => oo.OperationLocation)
            .Include(oo => oo.DailyProjectOperationDocuments)
            .Include(oo => oo.DailyProjectOperationServices)
               .ThenInclude(oo => oo.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo)

            .Where(c => c.ProjectOperationDetailId.Equals(projectOperationDetailId) &&
               (c.Length > 0 || c.Width > 0 || c.Height > 0 || c.Weight > 0 || c.Number > 0) &&
               (contractorId == null ||
                c.DailyProjectOperationServices.Any(x => x.ContractorId.Equals(contractorId)) ||
                c.DailyProjectOperationServices.Any(x => x.ProjectOperationDetailContractorService.ContractorId.Equals(contractorId))) &&
               (length == null || c.Length == length) &&
               (width == null || c.Width == width) &&
               (height == null || c.Height == height) &&
               (weight == null || c.Weight == weight) &&
               (number == null || c.Number == number) &&
               (creatorId == null || c.CreatorId == creatorId) &&
               (startDate == null || c.StartDate.Date >= startDate.Value.Date) &&
               (endDate == null || c.EndDate.Date <= endDate.Value.Date) &&
               (string.IsNullOrWhiteSpace(filterData) ||
                       EF.Functions.Like(c.ProjectOperationDetail.OperationLocation.PublicCode, filterData.MakeLikePattern()) ||
                       EF.Functions.Like(c.ProjectOperationDetail.Description, filterData.MakeLikePattern()) ||
                       EF.Functions.Like(c.Description, filterData.MakeLikePattern()) ||
                       EF.Functions.Like(c.ProjectOperationDetail.OperationLocation.PublicName, filterData.MakeLikePattern())));

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entites = await query.AsNoTracking()
            .ToListAsync(ct);

        return (entites, count);
    }

    public async Task<(List<DailyProjectOperation> Data, int RowCount)> GetFilteredDailyProjectOperationForExcel(
        List<long>? ids,
        long projectOperationDetailId,
        DateTime? startDate,
        DateTime? endDate,
        long? creatorId,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(oo => oo.ProjectOperationDetail.OperationLocation)
            .Include(oo => oo.ProjectOperationDetail.ProjectOperation)
            .Include(oo => oo.ProjectOperationDetail.ProjectOperationDetailDeductions)
            .Include(oo => oo.ProjectOperationDetail.ProjectOperation.OperationInfo)
            .Include(oo => oo.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters)
               .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.DailyProjectOperationDocuments)

            .Where(c => c.ProjectOperationDetailId.Equals(projectOperationDetailId) &&
                        (startDate == null || c.StartDate >= startDate) &&
                        (endDate == null || c.EndDate <= endDate) &&
                        (creatorId == null || c.CreatorId == creatorId) &&
                        (ids == null || ids.Count == 0 || ids.Contains(c.Id)) &&
                        (string.IsNullOrWhiteSpace(filterData) ||
                       EF.Functions.Like(c.ProjectOperationDetail.OperationLocation.PublicCode, filterData.MakeLikePattern()) ||
                       EF.Functions.Like(c.ProjectOperationDetail.Description, filterData.MakeLikePattern()) ||
                       EF.Functions.Like(c.Description, filterData.MakeLikePattern()) ||
                       EF.Functions.Like(c.ProjectOperationDetail.OperationLocation.PublicName, filterData.MakeLikePattern())));

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entites = await query.AsNoTracking()
            .ToListAsync(ct);

        return (entites, count);
    }

    public async Task<(List<long> Data, int RowCount)> GetsFilteredContractors(
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        CT ct)
    {
        var query = DbSet
            .Where(oo => !oo.IsDeleted &&
            oo.CreatorId != 0 &&
            (costCenterIds == null || costCenterIds.Count == 0 || oo.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
            (projectIds == null || projectIds.Count == 0 || projectIds.Contains(oo.ProjectOperationDetail.ProjectOperation.Project.Id)) &&
            (operationInfoIds == null || operationInfoIds.Count == 0 || operationInfoIds.Contains(oo.ProjectOperationDetail.ProjectOperation.OperationInfo.Id)) &&
            (projectOperationIds == null || projectOperationIds.Count == 0 || projectOperationIds.Contains(oo.ProjectOperationDetail.ProjectOperation.Id)) &&
            (projectOperationDetailIds == null || projectOperationDetailIds.Count == 0 || projectOperationDetailIds.Contains(oo.ProjectOperationDetail.Id)) &&
            (oo.DailyProjectOperationServices != null && oo.DailyProjectOperationServices.Count > 0))
            .SelectMany(x => x.DailyProjectOperationServices)
            .Select(c => c.ProjectOperationDetailContractorService)
            .Where(c => c.ContractorId != null && c.ContractorId > 0)
            .Select(c => (long)c.ContractorId!);

        var count = await query.CountAsync(ct);
        var items = await query.Distinct().ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsDetailedDailyProjectOperationModel> Data, int RowCount)> GetsDetailedDailyProjectOperation(
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
        int pageSize, CT ct)
    {
        var query = BuildQueryGetsDetailedDailyProjectOperation(ids,
            costCenterIds,
            projectIds,
            projectOperationIds,
            projectOperationDetailIds,
            contractorIds,
            serviceInfoIds,
            creatorIds,
            startDate,
            endDate,
            fromDate,
            toDate,
            projectOperationDetailStatus,
            status,
            filterData);

        query = query.OrderBy(a => a.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<List<GetDetailedDailyProjectOperationTotalsModel>> GetDetailedDailyProjectOperationTotals(
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
        CT ct)
    {
        var query = BuildQueryGetsDetailedDailyProjectOperationTotals(ids,
            costCenterIds,
            projectIds,
            projectOperationIds,
            projectOperationDetailIds,
            contractorIds,
            serviceInfoIds,
            creatorIds,
            startDate,
            endDate,
            fromDate,
            toDate,
            status,
            projectOperationDetailStatus,
            filterData);

        return await query.ToListAsync(ct);
    }

    public async Task<GetsDailyProjectOperationDocumentResponse?> GetsDailyProjectOperationDocument(
        long dailyProjectOperationId, CT ct)
    {
        var query = BuildQueryGetsDailyProjectOperationDocument(dailyProjectOperationId);

        var item = await query.AsNoTracking().SingleOrDefaultAsync();
        return item;
    }

    public async Task<(List<long> Data, int RowCount)> GetsDailyCreator(
        CT ct)
    {
        var query = DbSet
            .Where(x => !x.IsDeleted)
            .Select(c => c.CreatorId).Distinct();

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<int> GetsDailyCreatedCount(
        CT ct)
    {
        var today = DateTime.Today;
        return await DbSet
            .Where(x => !x.IsDeleted && x.StartDate.Date == today).CountAsync(ct);
    }

    public async Task<List<GetsDailyOperationCreatedByProjectReportModel>> GetsDailyOperationCreatedByProjectReport(
        CT ct)
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        var query = DbSet
            .Where(x => !x.IsDeleted &&
                        x.StartDate >= today &&
                        x.StartDate < tomorrow)
            .GroupBy(x => new
            {
                x.ProjectOperationDetail.ProjectOperation.ProjectId,
                x.ProjectOperationDetail.ProjectOperation.Project.ProjectName
            })
            .Select(g => new GetsDailyOperationCreatedByProjectReportModel
            {
                ProjectId = g.Key.ProjectId,
                ProjectName = g.Key.ProjectName,
                DailyOperationCount = g.Count()
            });

        return await query.ToListAsync(ct);
    }
}
