using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Extensions;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsServiceByProjectOperationDetailIds;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetAssignable;
using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReporting;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Persistence.Repositories.ProjectOperationDetails;

public partial class ProjectOperationDetailRepository : BaseRepository<EngineeringDBContext, ProjectOperationDetail>, IProjectOperationDetailRepository
{

    public async Task<(List<ProjectOperationDetailsModel> Data, int RowCount)> GetsByProjectOperationId(
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
        List<long>? technicalAssistantIds,
        long? creatorId,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct)
    {
#pragma warning disable CS8629 // Nullable value type may be null.
        var query = BuildQueryGetsByProjectOperationId(
            ids,
            projectOperationId,
            privateName,
            privateCode,
            filterData,
            employerId,
            status,
            contractorIds,
            createDate,
            startDate,
            endDate,
            serviceInfoIds,
            implementationAssistantIds,
            technicalAssistantIds,
            creatorId);
#pragma warning restore CS8629 // Nullable value type may be null.

        query = query.OrderByDescending(x => x.CreateDate);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsIncludeLessDetailByProjectOperationId(
        long projectOperationId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                x.ProjectOperation!.Id == projectOperationId);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsByProjectOperationIdForVolumes(
        long projectOperationId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Include(x => x.ConsumableVolumeExperts)
            .Include(x => x.ConsumableVolumeProducts)
            .Include(x => x.ConsumableVolumeMachineries)
                .ThenInclude(x => x.Machinery)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                x.ProjectOperation!.Id == projectOperationId);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsByProjectOperationIdInEmployerContract(
        long projectOperationId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Include(x => x.ConsumableVolumeExperts)
            .Include(x => x.ConsumableVolumeProducts)
            .Include(x => x.ConsumableVolumeMachineries)
                .ThenInclude(x => x.Machinery)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                x.ProjectOperation!.Id == projectOperationId);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsProjectOperationDetailReportingModel> Data, int RowCount)> GetsProjectOperationDetailReporting(
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
        int pageSize,
        CT ct)
    {
        var query = BuildQueryGetsProjectOperationDetailReporting(
            ids,
            startDate,
            endDate,
            createFrom,
            createTo,
            costCenterId,
            projectIds,
            operationInfoIds,
            projectOperationIds,
            contractorIds,
            statuses,
            locationFilterData,
            descriptionFilterData,
            dailyDescription,
            filterData
            );

        query = query.OrderByDescending(x => x.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsProjectOperationDetailByContractorIds(
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? projectOperationIds,
        List<long>? contractorIds,
        string? filterData,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation)
                .ThenInclude(x => x.Project)
                    .ThenInclude(x => x.ProjectCostCenters)
                        .ThenInclude(x => x.CostCenter)
            .Include(x => x.ProjectOperation)
                .ThenInclude(x => x.OperationInfo)
            .Include(x => x.OperationLocation)
            .Include(x => x.DailyOperations)
            .Include(x => x.ProjectOperationDetailContractorServices)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                (costCenterIds == null || costCenterIds.Count == 0 || x.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (projectIds == null || projectIds.Count == 0 || projectIds.Contains(x.ProjectOperation.Project.Id)) &&
                (operationInfoIds == null || operationInfoIds.Count == 0 || operationInfoIds.Contains(x.ProjectOperation.OperationInfo.Id)) &&
                (projectOperationIds == null || projectOperationIds.Count == 0 || projectOperationIds.Contains(x.ProjectOperation.Id)) &&
                (contractorIds == null || x.ProjectOperationDetailContractorServices.Any(x => x.ContractorId.HasValue && contractorIds.Contains(x.ContractorId.Value))) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                    EF.Functions.Like(x.Description, filterData.MakeLikePattern()) ||
                    EF.Functions.Like(x.OperationLocation.PublicCode, filterData.MakeLikePattern()) ||
                    EF.Functions.Like(x.OperationLocation.PublicName, filterData.MakeLikePattern())));

        query = query.OrderByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsByExpertId(
        long projectOperationId,
        long expertId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(e => e.ConsumableVolumeExperts.Where(e => e.ExpertId == expertId))
            .Include(x => x.ProjectOperation.Project)
            .Include(x => x.ProjectOperation.OperationInfo)
            .Include(x => x.OperationLocation)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                x.ProjectOperation!.Id == projectOperationId &&
                x.ConsumableVolumeExperts.Any(e => e.ExpertId == expertId));

        query = query
            .OrderByDescending(x => x.Priority != 0)
            .ThenBy(x => x.Priority)
            .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsProjectOperationDetailForScheduling(
        long operationInfoId,
        long operationLocationId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation.OperationInfo)
            .Include(x => x.OperationLocation)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                x.ProjectOperation!.OperationInfo.Id == operationInfoId &&
                x.OperationLocation.Id.Equals(operationLocationId));

        query = query
            .OrderByDescending(x => x.Priority != 0)
            .ThenBy(x => x.Priority)
            .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsByMachineryId(
        long projectOperationId,
        long machineryId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(e => e.ConsumableVolumeMachineries.Where(m => m.Machinery.Id == machineryId))
                .ThenInclude(x => x.Machinery)
            .Include(x => x.ProjectOperation)
                .ThenInclude(x => x.Project)
            .Include(x => x.ProjectOperation)
                .ThenInclude(x => x.OperationInfo)
            .Include(x => x.OperationLocation)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                x.ProjectOperation!.Id == projectOperationId &&
                x.ConsumableVolumeMachineries.Any(m => m.Machinery.Id == machineryId));

        query = query
            .OrderByDescending(x => x.Priority != 0)
            .ThenBy(x => x.Priority)
            .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsByProductId(
        long projectOperationId,
        long productId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(e => e.ConsumableVolumeProducts.Where(e => !e.IsDeleted && e.ProductGroupId == productId))
            .Include(x => x.ProjectOperation.Project)
            .Include(x => x.ProjectOperation.OperationInfo)
            .Include(x => x.OperationLocation)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                x.ProjectOperation!.Id == projectOperationId &&
                x.ConsumableVolumeProducts.Any(e => e.ProductGroupId == productId));

        query = query
            .OrderByDescending(x => x.Priority != 0)
            .ThenBy(x => x.Priority)
            .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetDailyProjectOperationDetailFilter(
        long projectOperationId,
        ProjectOperationDetailStatus? status,
        DateTime? startDate,
        DateTime? endDate,
        DateTime? createDate,
        List<long>? operationLocationIds,
        List<long>? serviceInfoIds,
        long? contractorId,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationLocation)
            .Include(x => x.ProjectOperationDetailContractorServices)
            .Include(x => x.DailyOperations)
            .Include(x => x.ProjectOperationDetailDeductions)
            .Include(x => x.ProjectOperation)
            .Include(x => x.ProjectOperation.Project)
            .Include(x => x.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(x => x.CostCenter)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                !x.IsDeleted &&
                x.ProjectOperation.Id.Equals(projectOperationId) &&
                (status == null || x.Status == status) &&
                (contractorId == null || x.ProjectOperationDetailContractorServices.Any(x => x.ContractorId.HasValue && x.ContractorId == contractorId)) &&
                (serviceInfoIds == null || x.ProjectOperationDetailContractorServices.Any(x => serviceInfoIds.Contains(x.OperationInfoService.ServiceInfo.Id))) &&
                (operationLocationIds == null || operationLocationIds.Contains(x.OperationLocation.Id)) &&
                (startDate == null || x.StartDate == null || x.StartDate.Value.Date >= startDate.Value.Date) &&
                (endDate == null || x.EndDate == null || x.EndDate.Value.Date <= endDate.Value.Date) &&
                (createDate == null || x.Created.Date == createDate.Value.Date) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                EF.Functions.Like(x.Description, filterData.MakeLikePattern()) ||
                EF.Functions.Like(x.OperationLocation.PublicCode, filterData.MakeLikePattern()) ||
                EF.Functions.Like(x.OperationLocation.PublicName, filterData.MakeLikePattern())))

        .Select(x => new { x, SortStatus = x.Status == ProjectOperationDetailStatus.Doing ? 1 : 2 })
        .Select(x => x.x);

        query = query
            .OrderByDescending(x => x.Priority != 0)
            .ThenBy(x => x.Priority)
            .ThenByDescending(x => x.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsByIds(
        List<long> projectOperationIds, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation.Project)
            .Include(x => x.ProjectOperation.OperationInfo)
            .Include(x => x.OperationLocation)
            .Include(x => x.UserImplementations)
            .Include(x => x.UserTechnicals)
            .Include(x => x.UserPlaners)
            .Include(x => x.ConsumableVolumeExperts)
            .Include(x => x.ConsumableVolumeProducts)
            .Include(x => x.ConsumableVolumeMachineries)
                .ThenInclude(x => x.Machinery)
            .Include(x => x.ProjectOperationDetailContractorServices)
                .ThenInclude(x => x.OperationInfoService.ServiceInfo)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                projectOperationIds.Contains(x.Id) && !x.IsDeleted);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<List<ProjectOperationDetail>> GetWithoutIncludeByIds(
        List<long> Ids, CT ct)
    {
        var query = DbSet

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                Ids.Contains(x.Id) && !x.IsDeleted);

        var items = await query.ToListAsync(ct);

        return (items);
    }


    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsByIdsIncludeless(
        List<long> projectOperationIds, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationLocation)
            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                projectOperationIds.Contains(x.Id) && !x.IsDeleted);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsForVolumesByIds(
        List<long>? projectOperationDetailIds, CT ct)
    {
        var query = DbSet
            .Include(x => x.ConsumableVolumeExperts)
            .Include(x => x.ConsumableVolumeProducts)
                .ThenInclude(x => x.RequestGoodsSupplyDetails)
            .Include(x => x.ConsumableVolumeMachineries)
                .ThenInclude(x => x.Machinery)
            .Include(x => x.ProjectOperation.OperationInfo)
                .ThenInclude(z => z.ConsumptionStandardProduct)
            .Include(x => x.ProjectOperation.OperationInfo)
                .ThenInclude(z => z.ConsumptionStandardExperts)
            .Include(x => x.ProjectOperation.OperationInfo)
                .ThenInclude(z => z.ConsumptionStandardMachineries)
                    .ThenInclude(m => m.Machinery)

        .Where(x =>
            x.OperationLocation!.IsDeleted == false &&
            (projectOperationDetailIds == null || projectOperationDetailIds.Contains(x.Id)) &&
            !x.IsDeleted);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsForVolumesByProjectOperationId(
        long? projectOperationId, CT ct)
    {
        var query = DbSet
            .Include(x => x.ConsumableVolumeExperts)
            .Include(x => x.ConsumableVolumeProducts)
                .ThenInclude(x => x.RequestGoodsSupplyDetails)
            .Include(x => x.ConsumableVolumeMachineries)
                .ThenInclude(x => x.Machinery)
            .Include(x => x.ProjectOperation.OperationInfo)
                .ThenInclude(z => z.ConsumptionStandardProduct)
            .Include(x => x.ProjectOperation.OperationInfo)
                .ThenInclude(z => z.ConsumptionStandardExperts)
            .Include(x => x.ProjectOperation.OperationInfo)
                .ThenInclude(z => z.ConsumptionStandardMachineries)
                    .ThenInclude(m => m.Machinery)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                (projectOperationId == null || x.ProjectOperation.Id == projectOperationId) &&
                !x.IsDeleted);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsSummarizedByProjectOperationIds(
        List<long> projectOperationIds,
        string? filterData,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation)
            .Include(x => x.OperationLocation)

        .Where(x =>
            x.OperationLocation!.IsDeleted == false &&
            projectOperationIds.Contains(x.ProjectOperation.Id) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.OperationLocation.PublicName, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.OperationLocation.PublicCode!, filterData.MakeLikePattern()))
            );

        query = query
            .OrderByDescending(x => x.Priority != 0)
            .ThenBy(x => x.Priority)
            .ThenByDescending(x => x.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsMinimalByProjectOperationIds(
        List<long>? projectOperationIds,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation.OperationInfo)
            .Include(x => x.OperationLocation)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                (companyId == null || x.CompanyId == companyId) &&
                (projectOperationIds == null || projectOperationIds.Contains(x.ProjectOperation.Id)));

        query = query
            .OrderByDescending(x => x.Priority != 0)
            .ThenBy(x => x.Priority)
            .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsMinimalByProjectOperationDetialIds(
        List<long>? ids,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperationDetailContractorServices)
                .ThenInclude(x => x.OperationInfoService.ServiceInfo)
            .Include(x => x.DailyOperations)
                 .ThenInclude(x => x.DailyProjectOperationProducts)
            .Include(x => x.RequestGoodsSupplies)
                .ThenInclude(z => z.RequestGoodsSupplyDetails)
                    .ThenInclude(y => y.ConsumableVolumeProduct)
            .Include(x => x.ConsumableVolumeExperts)
            .Include(x => x.ConsumableVolumeProducts)
            .Include(x => x.ConsumableVolumeMachineries)
                .ThenInclude(x => x.Machinery)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                (ids == null || ids.Contains(x.Id)));

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsProjectOperationDetailByIds(
        List<long>? ids,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation.OperationInfo)
            .Include(x => x.OperationLocation)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                (companyId == null || x.CompanyId == companyId) &&
                (ids == null || ids.Contains(x.Id)));

        query = query
            .OrderByDescending(x => x.Priority != 0)
            .ThenBy(x => x.Priority)
            .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsProjectOperationDetailData(
        long projectOperationId,
        VolumeProductType type,
        long productGroupId,
        long? projectOperationDetailId,
        string? filterData,
        long? contractorId,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationLocation)
            .Include(x => x.ProjectOperation)
                .ThenInclude(c => c.Project)
                    .ThenInclude(c => c.ProjectCategories)
                        .ThenInclude(d => d.Category)
            .Include(x => x.ProjectOperationDetailContractorServices)
            .Include(x => x.ConsumableVolumeProducts)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                !x.IsDeleted &&
                x.ProjectOperation.Id == projectOperationId &&
                (projectOperationDetailId == null || x.Id == projectOperationDetailId) &&
                (contractorId == null || x.ProjectOperationDetailContractorServices.Any(x => x.ContractorId == contractorId)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                    EF.Functions.Like(x.OperationLocation.PublicCode, filterData.MakeLikePattern()) ||
                    EF.Functions.Like(x.OperationLocation.PublicName, filterData.MakeLikePattern())));

        if (type == VolumeProductType.ProductGroup)
            query = query.Where(x =>
                x.ConsumableVolumeProducts.Any(p =>
                p.VolumeProductType == VolumeProductType.ProductGroup &&
                p.ProductGroupId == productGroupId));
        else if (type == VolumeProductType.Category)
            query = query.Where(x =>
                x.ConsumableVolumeProducts.Any(p =>
                p.VolumeProductType == VolumeProductType.Category &&
                p.ProductGroupId == productGroupId));

        query = query
            .OrderByDescending(x => x.Priority != 0)
            .ThenBy(x => x.Priority)
            .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetProjectOperationDetailsByRequestIdAsync(
        long projectOperationId,
        long productGroupId,
        long? projectOperationDetailId,
        string? filterData,
        long? contractorId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationLocation)
            .Include(x => x.ConsumableVolumeProducts)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                !x.IsDeleted &&
                x.ConsumableVolumeProducts.Any(s => s.ProductGroupId == productGroupId) &&
                x.ProjectOperation.Id == projectOperationId &&
                (projectOperationDetailId == null || x.Id == projectOperationDetailId) &&
                (contractorId == null || x.ProjectOperationDetailContractorServices.Any(x => x.ContractorId == contractorId)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                EF.Functions.Like(x.OperationLocation.PublicCode, filterData.MakeLikePattern()) ||
                EF.Functions.Like(x.OperationLocation.PublicName, filterData.MakeLikePattern())));

        query = query
            .OrderByDescending(x => x.Priority != 0)
            .ThenBy(x => x.Priority)
            .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<OperationLocation> Data, int RowCount)> GetOperationLocationForSchecdulingAsync(
        long costCenterId,
        long projectId,
        string? filterData,
        List<long>? operationInfoIds,
        List<long>? operationLocationIds,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationLocation)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                (x.Status == ProjectOperationDetailStatus.NotStarted || x.Status == ProjectOperationDetailStatus.Doing) &&
                x.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId) &&
                x.ProjectOperation.Project.Id.Equals(projectId) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.OperationLocation.PublicName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.OperationLocation.PublicCode!, filterData.MakeLikePattern())) &&
                (operationInfoIds == null || operationInfoIds.Contains(x.ProjectOperation.OperationInfo.Id)) &&
                (operationLocationIds == null || operationLocationIds.Contains(x.OperationLocation.Id)))
            .Select(x => x.OperationLocation)
            .Distinct();

        query = query
            .OrderByDescending(x => x.Priority != 0)
            .ThenBy(x => x.Priority)
            .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<OperationInfo> Data, int RowCount)> GetOperationInfoForSchecdulingAsync(
        long costCenterId,
        long projectId,
        string? filterData,
        List<long>? operationLocationIds,
        List<long>? operationInfoIds,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationLocation)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                (x.Status == ProjectOperationDetailStatus.NotStarted || x.Status == ProjectOperationDetailStatus.Doing) &&
                x.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId) &&
                x.ProjectOperation.Project.Id.Equals(projectId) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.ProjectOperation.OperationInfo.OperationInfoCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.ProjectOperation.OperationInfo.OperationInfoName, filterData.MakeLikePattern())) &&
                (operationLocationIds == null || operationLocationIds.Contains(x.OperationLocation.Id)) &&
                (operationInfoIds == null || operationInfoIds.Contains(x.ProjectOperation.OperationInfo.Id)))
            .Select(x => x.ProjectOperation.OperationInfo)
            .Distinct();

        query = query
            .OrderByDescending(x => x.Priority != 0)
            .ThenBy(x => x.Priority)
            .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<ProjectOperationDetail>> GetForSchecdulingAsync(
        long costCenterId,
        long projectId,
        long projectOperationId, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationLocation)
            .Include(x => x.ProjectOperation.OperationInfo.OperationInfoDependencies)
                .ThenInclude(x => x.OperationInfo)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                x.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId) &&
                x.ProjectOperation.Project.Id.Equals(projectId) &&
                x.ProjectOperation.Id.Equals(projectOperationId));

        return await query.ToListAsync(ct);
    }

    public async Task<List<ProjectOperationDetail>> ValidatesProjectOperationDetailForScheduling(
        List<long> operationInfoIds,
        List<long> operationLocationIds, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationLocation)
            .Include(x => x.ProjectOperation)
            .Include(x => x.ProjectOperation.OperationInfo.OperationInfoDependencies)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                operationInfoIds.Contains(x.ProjectOperation.OperationInfo.Id) &&
                operationLocationIds.Contains(x.OperationLocation.Id));

        return await query.ToListAsync(ct);
    }

    public async Task<(List<long> Data, int RowCount)> GetsFilteredContractors(
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds, CT ct)
    {
        var query = DbSet
            .Where(x => !x.IsDeleted &&
                x.CreatorId != 0 &&
                (projectOperationDetailIds == null || projectOperationDetailIds.Count == 0 || projectOperationDetailIds.Contains(x.Id)) &&
                (costCenterIds == null || costCenterIds.Count == 0 || x.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (projectIds == null || projectIds.Count == 0 || projectIds.Contains(x.ProjectOperation.Project.Id)) &&
                (operationInfoIds == null || operationInfoIds.Count == 0 || operationInfoIds.Contains(x.ProjectOperation.OperationInfo.Id)) &&
                (projectOperationIds == null || projectOperationIds.Count == 0 || projectOperationIds.Contains(x.ProjectOperation.Id)) &&
                (x.ProjectOperationDetailContractorServices != null && x.ProjectOperationDetailContractorServices.Count > 0))

        .SelectMany(x => x.ProjectOperationDetailContractorServices)
            .Where(x => x.ContractorId != null && x.ContractorId > 0).Select(x => (long)x.ContractorId!);

        var count = await query.CountAsync(ct);
        var items = await query.Distinct().ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsContractorProjectOperationDetailReports(
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
        int pageSize, CT ct)
    {
        var query = DbSet
              .Include(x => x.DailyOperations)
                .ThenInclude(x => x.DailyProjectOperationServices)
                    .ThenInclude(x => x.ProjectOperationDetailContractorService)
                        .ThenInclude(x => x.ContractorContractDetailServices)
                        .ThenInclude(x => x.ContractorContractDetail)
                            .ThenInclude(x => x.ContractorContract)
                                .ThenInclude(x => x.ContractorContractHeader)
            .Include(x => x.DailyOperations)
                .ThenInclude(x => x.DailyProjectOperationServices)
                    .ThenInclude(x => x.ProjectOperationDetailContractorService)
                        .ThenInclude(x => x.ContractorContractDetailServices)
                        .ThenInclude(x => x.ContractorContractDetail)
                            .ThenInclude(x => x.ContractorContract)
            .Include(x => x.DailyOperations)
                .ThenInclude(x => x.DailyProjectOperationServices)
                    .ThenInclude(x => x.ProjectOperationDetailContractorService)
                        .ThenInclude(x => x.OperationInfoService)
                            .ThenInclude(x => x.ServiceInfo)
            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                (x.DailyOperations.Any()) &&
                (ids == null || ids.Contains(x.Id)) &&
                (contractorId == null ||
                    x.DailyOperations.Any(x => x.DailyProjectOperationServices
                    .Any(x => x.ProjectOperationDetailContractorService.ContractorContractDetailServices
                    .Any(x => x.ContractorContractDetail.ContractorContract.ContractorContractHeader.ContractorId == contractorId))) &&

                (costCenterId == null || x.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (projectIds == null || projectIds.Count == 0 || projectIds.Contains(x.ProjectOperation.Project.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                EF.Functions.Like(x.Description, filterData.MakeLikePattern()) ||
                EF.Functions.Like(x.OperationLocation.PublicCode, filterData.MakeLikePattern()) ||
                EF.Functions.Like(x.OperationLocation.PublicName, filterData.MakeLikePattern())) &&
                (contractorContractIds == null || contractorContractIds.Count == 0 || x.DailyOperations
                    .Any(x => x.DailyProjectOperationServices
                    .Any(x => x.ProjectOperationDetailContractorService.ContractorContractDetailServices
                    .Any(x => contractorContractIds.Contains(x.ContractorContractDetail.ContractorContract.Id))))) &&

                (fromDate == null || x.StartDate == null || x.StartDate.Value.Date >= fromDate.Value.Date) &&
                (toDate == null || x.EndDate == null || x.EndDate.Value.Date <= toDate.Value.Date) &&
                (companyId == null || x.CompanyId == companyId)));

        query = query.OrderByDescending(x => x.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetail> Data, int RowCount)> GetsContractorProjectOperationDetailDetailReports(
        List<long>? ids,
        long? contractorContractId,
        long? contractorId,
        DateTime? fromDate,
        DateTime? toDate,
        long? companyId,
        string? filterData,
        int pageIndex,
        int pageSize, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
             .Include(x => x.OperationLocation)
             .Include(x => x.ProjectOperation.OperationInfo)
             .Include(x => x.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(x => x.CostCenter)
             .Include(x => x.DailyOperations)
                .ThenInclude(x => x.DailyProjectOperationServices)
                    .ThenInclude(x => x.ProjectOperationDetailContractorService)
                        .ThenInclude(x => x.ContractorContractDetailServices)
                        .ThenInclude(x => x.ContractorContractDetail)
                             .ThenInclude(x => x.ContractorContract)
                                .ThenInclude(x => x.ContractorContractHeader)
            .Include(x => x.DailyOperations)
                .ThenInclude(x => x.DailyProjectOperationServices)
                    .ThenInclude(x => x.ProjectOperationDetailContractorService)
                        .ThenInclude(x => x.ContractorContractDetailServices)
                        .ThenInclude(x => x.ContractorContractDetail)
            .Include(x => x.DailyOperations)
                .ThenInclude(x => x.DailyProjectOperationServices)
                    .ThenInclude(x => x.ProjectOperationDetailContractorService)
                        .ThenInclude(x => x.OperationInfoService)
                            .ThenInclude(x => x.ServiceInfo)
           .Include(x => x.DailyOperations)
                .ThenInclude(x => x.DailyProjectOperationServices)
                    .ThenInclude(x => x.ProjectOperationDetailContractorService)
                        .ThenInclude(x => x.ContractorContractDetailServices)
                        .ThenInclude(x => x.ContractorContractDetail)
                            .ThenInclude(x => x.ContractorContract).AsQueryable();

        query = query
            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                (x.DailyOperations.Any()) &&
                (ids == null || ids.Contains(x.Id)) &&
                (contractorId == null || x.DailyOperations
                    .Any(x => x.DailyProjectOperationServices
                    .Any(x => x.ProjectOperationDetailContractorService.ContractorContractDetailServices
                    .Any(x => x.ContractorContractDetail.ContractorContract.ContractorContractHeader.ContractorId == contractorId))) &&

                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.ProjectOperation.OperationInfo.OperationInfoCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.ProjectOperation.OperationInfo.OperationInfoName, filterData.MakeLikePattern())) &&

                (contractorContractId == null || x.DailyOperations
                    .Any(x => x.DailyProjectOperationServices
                    .Any(x => x.ProjectOperationDetailContractorService.ContractorContractDetailServices
                    .Any(x => x.ContractorContractDetail.ContractorContract.Id == contractorContractId)))) &&

                (fromDate == null || x.StartDate == null || x.StartDate.Value.Date >= fromDate.Value.Date) &&
                (toDate == null || x.EndDate == null || x.EndDate.Value.Date <= toDate.Value.Date) &&
                (companyId == null || x.CompanyId == companyId)));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(x => x.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<ProjectOperationDetail>> GetTotalsByProjectOperationId(
        long projectOperationId, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation)
            .Include(x => x.DailyOperations)
            .Include(x => x.ProjectOperationDetailDeductions)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                x.IsDeleted == false &&
                x.ProjectOperation.Id.Equals(projectOperationId));

        return await query.ToListAsync(ct);
    }

    public async Task<List<ProjectOperationDetail>> GetProjectOperationDetailByCategoryId(
        long projectOperationId,
        long categoryId,
        long? projectOperationDetailId,
        string? filterData,
        long? contractorId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation)
            .Include(x => x.OperationLocation)
            .Include(x => x.DailyOperations)
            .Include(x => x.ProjectOperationDetailDeductions)

            .Where(x =>
                x.OperationLocation!.IsDeleted == false &&
                x.IsDeleted == false &&
                x.ProjectOperation.Id.Equals(projectOperationId) &&
                (x.ProjectOperation.Project.ProjectCategories != null &&
                x.ProjectOperation.Project.ProjectCategories.HasItems() &&
                x.ProjectOperation.Project.ProjectCategories.Any(x => x.CategoryId == categoryId)) &&
                (projectOperationDetailId == null || x.Id == projectOperationDetailId) &&
                (contractorId == null || x.ProjectOperationDetailContractorServices.Any(x => x.ContractorId == contractorId)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                    EF.Functions.Like(x.OperationLocation.PublicCode, filterData.MakeLikePattern()) ||
                    EF.Functions.Like(x.OperationLocation.PublicName, filterData.MakeLikePattern())));

        return await query.ToListAsync(ct);
    }

    public async Task<(
    List<GetsServiceByProjectOperationDetailIdsModel> Data,
    int RowCount)>
    GetsServiceByProjectOperationDetailIds(
        List<long> projectOperationDetailIds,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .AsNoTracking()
            .Where(x =>
                !x.IsDeleted &&
                projectOperationDetailIds.Contains(x.Id))
            .Select(x => new GetsServiceByProjectOperationDetailIdsModel
            {
                ProjectOperationDetailId = x.Id,
                Description = x.Description,
                PublicCode = x.OperationLocation.PublicCode,
                PublicName = x.OperationLocation.PublicName,
                Created = x.Created,
                Services = null
            })
            .OrderByDescending(x => x.Created);

        var count = await query.CountAsync(ct);

        IQueryable<GetsServiceByProjectOperationDetailIdsModel> pagedQuery =
            query;

        if (pageIndex > 0 || pageSize > 0)
        {
            pagedQuery = pagedQuery.Page(
                pageIndex,
                pageSize);
        }

        var items = await pagedQuery.ToListAsync(ct);

        if (items.Count == 0)
        {
            return (items, count);
        }

        var detailIds = items
            .Where(x => x.ProjectOperationDetailId.HasValue)
            .Select(x => x.ProjectOperationDetailId!.Value)
            .ToList();

        var detailsWithServices = await DbSet
            .AsNoTracking()
            .Where(x =>
                !x.IsDeleted &&
                detailIds.Contains(x.Id))
            .Include(x =>
                x.ProjectOperationDetailContractorServices)
                .ThenInclude(x =>
                    x.OperationInfoService!)
                    .ThenInclude(x =>
                        x.ServiceInfo)
            //.Include(x =>
            //    x.RequestContractors)
            .AsSplitQuery()
            .ToListAsync(ct);

        var servicesByDetail =
            detailsWithServices
                .ToDictionary(
                    x => x.Id,
                    x => x.ProjectOperationDetailContractorServices
                        .Where(service =>
                            !service.IsDeleted)
                        .Select(service =>
                        {
                            var operationInfoService =
                                service.OperationInfoService;

                            var serviceInfo =
                                operationInfoService?.ServiceInfo;

                            return new GetServiceDataRequestModel
                            {
                                Id = service.Id,

                                ServiceInfoId =
                                    serviceInfo?.Id,

                                ServiceInfoName =
                                    serviceInfo?.ServiceInfoName,

                                ServiceInfoCode =
                                    serviceInfo?.ServiceInfoCode,

                                Volume =
                                    service.Volume,

                                Type =
                                    service.Type,

                                /*HaveRequest =
                                    service.Type ==
                                        PODContractorServiceType.ServiceBased &&
                                    serviceInfo is not null &&
                                    x.RequestContractors.Any(r =>
                                        !r.IsDeleted &&
                                        r.ServiceInfoId ==
                                            serviceInfo.Id)*/
                                HaveRequest = false
                            };
                        })
                        .ToList());

        foreach (var item in items)
        {
            if (item.ProjectOperationDetailId.HasValue &&
                servicesByDetail.TryGetValue(
                    item.ProjectOperationDetailId.Value,
                    out var services))
            {
                item.Services = services;
            }
            else
            {
                item.Services = [];
            }
        }

        return (items, count);
    }

    public async Task<(
    List<AssignablePODModel> Data,
    int RowCount)>
    GetsAssignableOperationBasedDetailsByProjectOperationId(
        long projectOperationId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var baseQuery = DbSet
            .Where(x =>
                !x.IsDeleted &&
                x.ProjectOperationId == projectOperationId &&

                !x.ProjectOperationDetailContractorServices.Any(s =>
                    !s.IsDeleted &&
                    s.Type == PODContractorServiceType.ServiceBased))
            .Select(x => new
            {
                x.Id,
                x.Code,
                x.Description,
                x.FinalAmount,

                DeductionVolume =
                    x.ProjectOperationDetailDeductions
                        .Where(d => !d.IsDeleted)
                        .Sum(d => (decimal?)(
                            d.Length *
                            d.Width *
                            d.Height *
                            d.Weight *
                            d.Number)) ?? 0m,

                AssignedVolume =
                    x.ProjectOperationDetailContractorServices
                        .Where(s =>
                            !s.IsDeleted &&
                            s.Type == PODContractorServiceType.OperationBased)
                        .Sum(s => (decimal?)s.Volume) ?? 0m
            });

        var query = baseQuery
            .Where(x =>
                x.FinalAmount -
                x.DeductionVolume -
                x.AssignedVolume > 0)
            .Select(x => new AssignablePODModel
            {
                ProjectOperationDetailId = x.Id,

                Code = x.Code,
                Description = x.Description,

                FinalAmount = x.FinalAmount,
                DeductionVolume = x.DeductionVolume,

                AssignableVolume =
                    x.FinalAmount - x.DeductionVolume,

                AssignedVolume = x.AssignedVolume,

                RemainingVolume =
                    x.FinalAmount -
                    x.DeductionVolume -
                    x.AssignedVolume
            });

        var rowCount = await query.CountAsync(ct);

        query = query.OrderByDescending(x => x.ProjectOperationDetailId);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var data = await query.ToListAsync(ct);

        return (data, rowCount);
    }

    public async Task<List<ProjectOperationDetail>> GetsForOperationBasedAssignmentByIds(
        long projectOperationId,
        List<long> projectOperationDetailIds,
        CT ct)
    {
        return await DbSet
            .Include(x => x.ProjectOperationDetailDeductions)
            .Include(x => x.ProjectOperationDetailContractorServices)
            .Where(x =>
                !x.IsDeleted &&
                x.ProjectOperationId == projectOperationId &&
                projectOperationDetailIds.Contains(x.Id))
            .ToListAsync(ct);
    }
}
