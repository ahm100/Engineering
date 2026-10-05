using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsPartialProjectOperationDetailService;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetInfoByContractorServiceId;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetByPO;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectContractors;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;
using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.Contracts.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Persistence.Repositories.ProjectOperationDetails;

public class ProjectOperationDetailContractorServiceRepository : BaseRepository<EngineeringDBContext, ProjectOperationDetailContractorService>, IProjectOperationDetailContractorServiceRepository
{
    public ProjectOperationDetailContractorServiceRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectOperationDetailContractorService?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectServiceDetail)
                .ThenInclude(oo => oo.ProjectService)
            .Include(c => c.OperationInfoService.ServiceInfo)
            .Include(c => c.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(c => c.CostCenter)
            .Include(c => c.ProjectOperationDetail.ProjectOperation.OperationInfo)
            .Include(p => p.ProjectOperationDetail.OperationLocation)
            .Where(c => c.Id.Equals(id) && !c.IsDeleted);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<ProjectOperationDetailContractorService?> GetByDetailServiceIds(
    long projectOperationDetailId,
    long serviceInfoId,
    CT ct)
    {
        var query = DbSet
            .Where(c =>
                c.Type == PODContractorServiceType.ServiceBased &&
                c.OperationInfoServiceId > 0 &&
                c.ProjectOperationDetail.Id == projectOperationDetailId &&
                c.OperationInfoService!.ServiceInfo.Id == serviceInfoId &&
                !c.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ProjectOperationDetailContractorService?> FindForDelete(long id, CT ct)
    {
        var query = DbSet
            .Include(c => c.ContractorContractDetailServices)
            .ThenInclude(c => c.ContractorContractDetail)
            .Include(p => p.DailyOperationServices)
                .ThenInclude(x => x.DailyProjectOperation)
                    .ThenInclude(z => z.ProjectOperationDetail)
            .Where(c => c.Id.Equals(id) && !c.IsDeleted);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<(List<GetsContractorServiceByProjectOperationDetailIdModel> Data, int RowCount)> GetsContractorServiceByProjectOperationDetailId(
        long projectOperationDetailId,
        string? serviceInfoName,
        string? serviceInfoCode,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet

            .Where(cs =>
            cs.Type == PODContractorServiceType.ServiceBased &&
            cs.OperationInfoService != null &&
            !cs.IsDeleted &&
            !cs.ProjectOperationDetail.IsDeleted &&
            !cs.OperationInfoService.IsDeleted &&
            !cs.OperationInfoService.ServiceInfo.IsDeleted &&
            cs.OperationInfoService.ServiceInfo.ServiceInfoName != "خدمات پیمانکاری - از سیستم ساتک" &&
            cs.ProjectOperationDetail.Id == projectOperationDetailId &&
            (serviceInfoName == null ||
                cs.OperationInfoService.ServiceInfo.ServiceInfoName.Contains(serviceInfoName)) &&
            (serviceInfoCode == null ||
            cs.OperationInfoService.ServiceInfo.ServiceInfoCode.Contains(serviceInfoCode))).Select(item => new GetsContractorServiceByProjectOperationDetailIdModel()
            {
                Id = item.Id,
                Created = item.Created,
                ContractorId = item.ContractorId,
                Volume = item.Volume,
                Status = item.Status,
                IsActive = item.IsActive,
                TimeSpantLong = item.TimeSpant,
                UsedVolume = item.DailyOperationServices.Sum(x => x.Volume),
                ProjectOperationDetailId = item.ProjectOperationDetail.Id,
                ServiceInfoId = item.OperationInfoService.ServiceInfo.Id,
                ServiceInfoName = item.OperationInfoService.ServiceInfo.ServiceInfoName,
                ServiceInfoCode = item.OperationInfoService.ServiceInfo.ServiceInfoCode,
                UnitOfMeasurementId = item.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                ProjectServiceDetailId = item.ProjectServiceDetail.Id,
                ProjectServiceId = item.ProjectServiceDetail.ProjectService.Id,
                ProjectServiceName = item.ProjectServiceDetail.ProjectService.ServiceInfo.ServiceInfoName,
                ProjectServiceUnitOfMeasurementId = item.ProjectServiceDetail.ProjectService.ServiceInfo.UnitOfMeasurementId,
                ProjectServiceVolume = item.ProjectServiceDetail.ProjectService.Volume,
                ProjectServiceDoneVolume = item.ProjectServiceDetail.ProjectService.DoneVolume,
                HaveContractorStatusStatements = item.DailyOperationServices.Any(x => x.ContractorStatusStatementServiceDailies.Any()),
                ContractorStatusStatementsVolume = item.DailyOperationServices
                    .Where(x => x.ContractorStatusStatementServiceDailies.Any(x =>
                        !CSSStatusRules.AllowForDelete
                            .Contains(x.ContractorStatusStatementService.ContractorStatusStatementDetail.ContractorStatusStatement.Status)))
                    .Sum(s => s.Volume),
                Type = item.Type,
            });
#pragma warning restore CS8602 // Dereference of a possibly null reference.


        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)> GetsContractorServiceByProjectOperationId(long projectOperationId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.OperationInfoService.ServiceInfo)

            .Where(x =>
                   x.Type == PODContractorServiceType.ServiceBased &&
                   x.OperationInfoService != null &&
                   x.ProjectOperationDetail.ProjectOperation.Id == projectOperationId &&
                   !x.ProjectOperationDetail.IsDeleted &&
                   !x.IsDeleted);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)> GetsContractorServiceForDaily(long projectOperationDetailId, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.OperationInfoService.ServiceInfo)
            .Include(oo => oo.ProjectOperationDetail)

            .Where(oo => oo.ProjectOperationDetail.Id.Equals(projectOperationDetailId) &&
              !oo.ProjectOperationDetail.IsDeleted
            );

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<ProjectOperationDetailContractorService>> GetsDetailContractorServiceByIds(List<long> ids, CT ct)
    {
        var query = DbSet
            .Where(oo => ids.Contains(oo.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)> GetsByFilter(string? filterData, long? costCenterId, long? projectId, List<long>? projectOperationIds,
        List<long>? serviceInfoIds, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(p => p.ProjectOperationDetail)
                .ThenInclude(p => p.ProjectOperation)
                    .ThenInclude(p => p.Project)
                        .ThenInclude(p => p.ProjectCostCenters)
                            .ThenInclude(p => p.CostCenter)
            .Include(p => p.ProjectOperationDetail)
                .ThenInclude(p => p.ProjectOperation)
                    .ThenInclude(p => p.OperationInfo)
            .Include(p => p.ProjectOperationDetail)
                .ThenInclude(p => p.OperationLocation)
            .Include(p => p.OperationInfoService.ServiceInfo)

            .Where(p =>
                p.Type == PODContractorServiceType.ServiceBased &&
                p.OperationInfoService != null &&
                !p.IsDeleted &&
                !p.ProjectOperationDetail.IsDeleted &&
                (costCenterId == null || p.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (projectId == null || p.ProjectOperationDetail.ProjectOperation.Project.Id.Equals(projectId)) &&
                (projectOperationIds == null || projectOperationIds.Contains(p.ProjectOperationDetail.ProjectOperation.Id)) &&
                (serviceInfoIds == null || serviceInfoIds.Contains(p.OperationInfoService.ServiceInfo.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.OperationInfoService.ServiceInfo.ServiceInfoCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.OperationInfoService.ServiceInfo.ServiceInfoName, filterData.MakeLikePattern())));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<long?> Data, int RowCount)> GetsProjectOperationDetailContractors(long? costCenterId, long? projectId, long? projectOperationId, long? projectOperationDetailId, CT ct)
    {
        var query = DbSet
            .Where(oo =>
                (costCenterId == null || oo.ProjectOperationDetail.ProjectOperation!.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (projectId == null || oo.ProjectOperationDetail.ProjectOperation.Project.Id == projectId) &&
                (projectOperationId == null || oo.ProjectOperationDetail.ProjectOperation.Id == projectOperationId) &&
                (projectOperationDetailId == null || oo.ProjectOperationDetail.Id == projectOperationDetailId))

            .Select(c => c.ContractorId).Distinct();

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<long?> Data, int RowCount)> GetFilteredProjectOperationDetailContractors(
        List<long> costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        CT ct)
    {
        var query = DbSet
            .Where(oo =>
                (oo.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&

                (projectIds == null ||
                (projectIds.Contains(oo.ProjectOperationDetail.ProjectOperation.Project.Id) && !oo.ProjectOperationDetail.ProjectOperation.Project.IsDeleted)) &&

                (projectOperationIds == null ||
                (projectOperationIds.Contains(oo.ProjectOperationDetail.ProjectOperation.Id) && !oo.ProjectOperationDetail.ProjectOperation.IsDeleted)) &&

                (projectOperationDetailIds == null ||
                (projectOperationDetailIds.Contains(oo.ProjectOperationDetail.Id) && !oo.ProjectOperationDetail.IsDeleted)))

            .Select(c => c.ContractorId).Distinct();

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<long> Data, int RowCount)> GetsContractorProjectService(
        long? projectId,
        long? serviceId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(oo =>
                oo.Type == PODContractorServiceType.ServiceBased &&
                oo.OperationInfoService != null &&
                !oo.IsDeleted &&
                !oo.ProjectOperationDetail.IsDeleted &&
                !oo.ProjectOperationDetail.ProjectOperation.IsDeleted &&
                oo.ContractorId.HasValue &&

                (projectId == null ||
                    (oo.ProjectOperationDetail.ProjectOperation.Project.Id == projectId &&
                     !oo.ProjectOperationDetail.ProjectOperation.Project.IsDeleted)) &&

                (serviceId == null ||
                    oo.OperationInfoService.ServiceInfo.Id == serviceId))
            .Select(c => c.ContractorId!.Value)
            .Distinct();

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<long>> GetFilteredContractors(
        long projectId,
        List<long>? projectOperationIds,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectServiceDetail)
                .ThenInclude(oo => oo.ProjectService)
            .Include(x => x.ProjectOperationDetail)
                .ThenInclude(oo => oo.ProjectOperation)
            .Include(x => x.ProjectOperationDetail)
               .ThenInclude(oo => oo.ProjectOperation)
                   .ThenInclude(oo => oo.Project)

            .Where(x =>
                x.ContractorId != null &&
                x.ContractorId != 0 &&
                !x.IsDeleted);

#pragma warning disable CS8629 // Nullable value type may be null.
        var result = query
            .GroupBy(x => x.ContractorId.Value)

            .Where(service =>
                (projectOperationIds == null || projectOperationIds.All(projectOperationId =>
                    service.Any(c => c.ProjectOperationDetail.ProjectOperation.Id == projectOperationId))) &&
                    service.Any(c => c.ProjectOperationDetail.ProjectOperation.Project.Id == projectId))
            .Select(contractor => contractor.Key);
#pragma warning restore CS8629 // Nullable value type may be null.

        return await result.ToListAsync(ct);
    }

    public async Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)> GetsIntegratedProjectOperationDetailService(
        List<long>? projectOperationDetailServiceIds,
        long? costCenterId,
        long? projectId,
        long? contractorId,
        List<long>? projectOperationIds,
        List<long>? serviceInfoIds,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet

            .Include(c => c.ProjectOperationDetail.ProjectOperation.OperationInfo)
            .Include(c => c.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(c => c.CostCenter)
            .Include(c => c.ProjectOperationDetail.OperationLocation)
            .Include(c => c.ProjectOperationDetail.ConsumableVolumeExperts)
            .Include(c => c.OperationInfoService.ServiceInfo)
            .Include(c => c.OperationInfoService.OperationInfo)
            .Include(c => c.ProjectServiceDetail!.ProjectService.ServiceInfo)
            .Include(c => c.ProjectOperationDetailContractorExperts)

            .Where(c =>
                c.Type == PODContractorServiceType.ServiceBased &&
                c.OperationInfoService != null &&
                !c.IsDeleted &&
                c.Status == ContractorServiceStatus.New &&
                c.IsActive &&
                (companyId == null || c.ProjectOperationDetail.CompanyId == companyId) &&
                (projectOperationDetailServiceIds == null || projectOperationDetailServiceIds.Contains(c.Id)) &&
                (serviceInfoIds == null || !serviceInfoIds.Contains(c.OperationInfoService.ServiceInfo.Id)) &&
                (costCenterId == null || c.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (projectId == null || c.ProjectOperationDetail.ProjectOperation.Project.Id.Equals(projectId)) &&
                (contractorId == null || c.ContractorId.Equals(contractorId)) &&
                (projectOperationIds == null || projectOperationIds.Contains(c.ProjectOperationDetail.ProjectOperation.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.OperationInfoService.ServiceInfo.ServiceInfoCode, filterData.MakeLikePattern()) ||
                EF.Functions.Like(c.OperationInfoService.ServiceInfo.ServiceInfoName, filterData.MakeLikePattern()))
                );

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<GetsPartialProjectOperationDetailServiceModel>> GetsPartialProjectOperationDetailService(
        long? costCenterId,
        long? projectId,
        long? contractorId,
        long serviceInfoId,
        List<long>? projectOperationDetailServiceIds,
        string? filterData,
        long? companyId,
        CT ct)
    {
        var query = DbSet
            .Where(c =>
                c.Type == PODContractorServiceType.ServiceBased &&
                c.OperationInfoService != null &&
                c.OperationInfoService.ServiceInfo.Id == serviceInfoId &&
                c.Status == ContractorServiceStatus.New &&
                !c.IsDeleted &&
                c.IsActive &&
                (companyId == null || c.ProjectOperationDetail.CompanyId == companyId) &&
                (projectOperationDetailServiceIds == null ||
                    !projectOperationDetailServiceIds.Contains(c.Id)) &&
                (costCenterId == null ||
                    c.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters
                        .Any(x => x.CostCenterId == costCenterId)) &&
                (projectId == null ||
                    c.ProjectOperationDetail.ProjectOperation.Project.Id == projectId) &&
                (contractorId == null ||
                    c.ContractorId == contractorId) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                    EF.Functions.Like(
                        c.OperationInfoService.ServiceInfo.ServiceInfoCode,
                        filterData.MakeLikePattern()) ||
                    EF.Functions.Like(
                        c.OperationInfoService.ServiceInfo.ServiceInfoName,
                        filterData.MakeLikePattern())))

            .Select(service => new GetsPartialProjectOperationDetailServiceModel()
            {
                ProjectOperationDetailServiceId = service.Id,
                ServiceInfoId = service.OperationInfoService.ServiceInfo.Id,
                ServiceInfoName = service.OperationInfoService.ServiceInfo.ServiceInfoName,
                ServiceInfoCode = service.OperationInfoService.ServiceInfo.ServiceInfoCode,
                UnitOfMeasurementId = service.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                OperationInfoName = service.OperationInfoService.OperationInfo.OperationInfoName,
                OperationInfoCode = service.OperationInfoService.OperationInfo.OperationInfoCode,
                ServiceInfoVolume = service.Volume,
                OperationLocationPrivateName = service.ProjectOperationDetail.OperationLocation.PrivateName,
                OperationLocationPrivateCode = service.ProjectOperationDetail.OperationLocation.PrivateCode,
                OperationLocationPublicName = service.ProjectOperationDetail.OperationLocation.PublicName,
                OperationLocationPublicCode = service.ProjectOperationDetail.OperationLocation.PublicCode,
                Description = service.ProjectOperationDetail.Description,
                StartDate = service.ProjectOperationDetail.StartDate,
                EndDate = service.ProjectOperationDetail.EndDate,
            });

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)> GetsProjectOperationDetailContractorService(
        List<long>? serviceIds,
        List<long>? projectServiceIds,
        List<long>? projectOperationDetailServiceIds,
        long? projectId,
        long? contractorId,
        List<long>? projectOperationIds,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(c => c.ProjectOperationDetail)
            .Include(c => c.OperationInfoService.ServiceInfo)
            .Include(c => c.ProjectServiceDetail.ProjectService.ServiceInfo)

            .Where(c =>
                c.Type == PODContractorServiceType.ServiceBased &&
                c.OperationInfoService != null &&
                c.Status == ContractorServiceStatus.New &&
                c.IsActive &&
                !c.IsDeleted &&
                (companyId == null || c.ProjectOperationDetail.CompanyId == companyId) &&
                (serviceIds == null || serviceIds.Contains(c.OperationInfoService.ServiceInfo.Id)) &&
                (projectServiceIds == null || projectServiceIds.Contains(c.ProjectServiceDetail.ProjectService.Id)) &&
                (projectOperationDetailServiceIds == null || projectOperationDetailServiceIds.Contains(c.Id)) &&
                (projectId == null || c.ProjectOperationDetail.ProjectOperation.Project.Id.Equals(projectId)) &&
                (contractorId == null || c.ContractorId == contractorId) &&
                (projectOperationIds == null || projectOperationIds.Contains(c.ProjectOperationDetail.ProjectOperation.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.OperationInfoService.ServiceInfo.ServiceInfoCode, filterData.MakeLikePattern()) ||
                EF.Functions.Like(c.OperationInfoService.ServiceInfo.ServiceInfoName, filterData.MakeLikePattern())));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)> GetsContractorServiceFiltered(
        List<long>? serviceIds,
        List<long>? projectOperationDetailServiceIds,
        long? projectId,
        long? contractorId,
        List<long>? projectOperationIds,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(c => c.ProjectOperationDetail.ProjectOperation.OperationInfo)
            .Include(c => c.ProjectOperationDetail.ProjectOperation)
            .Include(c => c.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(c => c.CostCenter)
            .Include(c => c.ProjectOperationDetail.OperationLocation)
            .Include(c => c.OperationInfoService.ServiceInfo)

            .Where(c =>
                c.Type == PODContractorServiceType.ServiceBased &&
                c.OperationInfoService != null &&
                (companyId == null || c.ProjectOperationDetail.CompanyId == companyId) &&
                (projectOperationDetailServiceIds == null ||
                    projectOperationDetailServiceIds.Contains(c.Id)) &&
                (serviceIds == null ||
                    serviceIds.Contains(c.OperationInfoService.ServiceInfo.Id)) &&
                (projectId == null ||
                    c.ProjectOperationDetail.ProjectOperation.Project.Id == projectId) &&
                (contractorId == null ||
                    c.ContractorId == contractorId) &&
                (projectOperationIds == null ||
                    projectOperationIds.Contains(c.ProjectOperationDetail.ProjectOperation.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                    EF.Functions.Like(
                        c.OperationInfoService.ServiceInfo.ServiceInfoCode,
                        filterData.MakeLikePattern()) ||
                    EF.Functions.Like(
                        c.OperationInfoService.ServiceInfo.ServiceInfoName,
                        filterData.MakeLikePattern())) &&
                !c.IsDeleted);

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)>
    GetsRequestedOperationContract(
        List<long>? projectOperationDetailServiceIds,
        long? companyId,
        CT ct)
    {
        var query = DbSet
            .Include(c => c.ProjectOperationDetail.ProjectOperation.OperationInfo)
            .Include(c => c.ProjectOperationDetail.ProjectOperation)
            .Where(c =>
                c.Type == PODContractorServiceType.OperationBased &&
                (companyId == null ||
                    c.ProjectOperationDetail.CompanyId == companyId) &&
                (projectOperationDetailServiceIds == null ||
                    projectOperationDetailServiceIds.Contains(c.Id)) &&
                !c.IsDeleted);

        var count = await query.CountAsync(ct);
        var result = await query.ToListAsync(ct);

        return (result, count);
    }

    public async Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)>
    GetsRequestedServiceContract(
        long projectId,
        List<long> serviceIds,
        long contractorId,
        long? companyId,
        CT ct)
    {
        var query = DbSet
            .Include(c => c.OperationInfoService!.ServiceInfo)
            .Include(c => c.ProjectOperationDetail.OperationLocation)
            .Include(c => c.ProjectOperationDetail.ProjectOperation.OperationInfo)
            .Include(c => c.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(c => c.CostCenter)

            .Where(c =>
                c.Type == PODContractorServiceType.ServiceBased &&
                c.OperationInfoService != null &&
                (companyId == null ||
                    c.ProjectOperationDetail.CompanyId == companyId) &&
                c.ProjectOperationDetail.ProjectOperation.Project.Id == projectId &&
                serviceIds.Contains(c.OperationInfoService.ServiceInfo.Id) &&
                c.ContractorId == contractorId &&
                !c.IsDeleted &&
                c.Status == ContractorServiceStatus.New);

        var count = await query.CountAsync(ct);
        var result = await query.ToListAsync(ct);

        return (result, count);
    }

    public async Task<List<GetInfoByContractorServiceIdResponse>> GetInfoByContractorServiceId(
    List<long?> ProjectOperationsDetailServiceIds,
    CT ct)
    {
        var query = DbSet
            .Where(c =>
                c.Type == PODContractorServiceType.ServiceBased &&
                c.OperationInfoService != null &&
                ProjectOperationsDetailServiceIds.Contains(c.Id))

            .Select(oo => new GetInfoByContractorServiceIdResponse()
            {
                Id = oo.Id,
                ServiceId = oo.OperationInfoService!.ServiceInfo.Id,
                ServiceName = oo.OperationInfoService.ServiceInfo.ServiceInfoName,
                ProjectOperationId = oo.ProjectOperationDetail.ProjectOperation.Id,
                ProjectOperationName =
                    oo.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                ProjectOperationDetailId = oo.ProjectOperationDetail.Id,
                ProjectOperationDetailName = oo.ProjectOperationDetail.Description,
            });

        query = query.OrderByDescending(oo => oo.ServiceId);

        return await query.ToListAsync(ct);
    }

    public async Task<(List<GetProjectContractorsModel>? Data, int RowCount)> GetProjectContractors(long projectId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(c => c.ProjectOperationDetail.ProjectOperation.ProjectId == projectId && !c.IsDeleted)
            .Select(x => new GetProjectContractorsModel
            {
                ContractorId = x.ContractorId
            });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);
        return (newQuery, count);
    }

    public async Task<(List<OpAssignModel> Data, int RowCount)> GetsOperationBasedAssignmentsByProjectOperationId(
        long projectOperationId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        IQueryable<OpAssignModel> query =
            DbSet
                .Where(x =>
                    !x.IsDeleted &&
                    !x.ProjectOperationDetail.IsDeleted &&
                    x.Type == PODContractorServiceType.OperationBased &&
                    x.ProjectOperationDetail.ProjectOperation.Id == projectOperationId)
                .Select(x => new OpAssignModel
                {
                    Id = x.Id,

                    ProjectOperationDetailId = x.ProjectOperationDetail.Id,
                    ProjectOperationDetailCode = x.ProjectOperationDetail.Code,
                    ProjectOperationDetailDescription = x.ProjectOperationDetail.Description,

                    ContractorId = x.ContractorId,
                    Volume = x.Volume,

                    HasContract = x.ContractorContractDetailServices
                        .Any(c => !c.IsDeleted),

                    CanEdit = !x.ContractorContractDetailServices
                        .Any(c => !c.IsDeleted),

                    CanDelete =
                        !x.ContractorContractDetailServices.Any(c => !c.IsDeleted) &&
                        !x.DailyOperationServices.Any(d => !d.IsDeleted)
                });

        var rowCount = await query.CountAsync(ct);

        query = query.OrderByDescending(x => x.Id);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var data = await query.ToListAsync(ct);

        return (data, rowCount);
    }

    public async Task<ProjectOperationDetailContractorService?> GetOperationBasedAssignmentForUpdate(
        long id,
        CT ct)
    {
        return await DbSet
            .Include(x => x.ContractorContractDetailServices)
            .Include(x => x.ProjectOperationDetail)
                .ThenInclude(x => x.ProjectOperationDetailDeductions)
            .Include(x => x.ProjectOperationDetail)
                .ThenInclude(x => x.ProjectOperationDetailContractorServices)
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                !x.IsDeleted &&
                x.Type == PODContractorServiceType.OperationBased,
                ct);
    }

    public async Task<decimal> GetContractAllocatedConstructionQuantity(
        long projectOperationDetailId,
        long? excludedContractTypeDetailId,
        CT ct)
    {
        var items = DbContext.Set<ContractChangeItem>().AsNoTracking();
        var baselineQuantity = await DbContext
            .Set<ContractTypeDetail>()
            .AsNoTracking()
            .Where(oo =>
                !oo.IsDeleted &&
                !oo.ContractType.IsDeleted &&
                oo.ProjectOperationDetailId == projectOperationDetailId &&
                (!excludedContractTypeDetailId.HasValue ||
                 oo.Id != excludedContractTypeDetailId.Value))
            .SumAsync(
                detail => detail.ContractType.PricingMethod == PricingMethod.LumpSum
                    ? (decimal?)detail.Quantity
                    : items
                        .Where(item => item.ContractTypeDetailId == detail.Id)
                        .OrderByDescending(item => item.ContractChange.Date)
                        .ThenByDescending(item => item.ContractChangeId)
                        .Select(item => (decimal?)item.NewValue)
                        .FirstOrDefault() ?? detail.Quantity,
                ct) ?? 0m;

        var sourceQuantity = await items
            .Where(item =>
                item.ContractTypeDetailId == null &&
                item.ProjectOperationDetailId == projectOperationDetailId &&
                !items.Any(later =>
                    later.ContractTypeDetailId == null &&
                    later.ContractChange.ContractId == item.ContractChange.ContractId &&
                    later.ContractTypeId == item.ContractTypeId &&
                    later.ProjectOperationDetailId == item.ProjectOperationDetailId &&
                    (later.ContractChange.Date > item.ContractChange.Date ||
                     later.ContractChange.Date == item.ContractChange.Date &&
                     later.ContractChangeId > item.ContractChangeId)))
            .SumAsync(item => (decimal?)item.NewValue, ct) ?? 0m;

        return baselineQuantity + sourceQuantity;
    }
}
