using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.ProjectServices.Models.GetProjectServiceById;
using Engineering.Application.Services.ProjectServices.Models.GetsProjectService;
using Engineering.Application.Services.ProjectServices.Models.ProjectServiceModels;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Repositories.Projects;

public class ProjectServiceRepository : BaseRepository<EngineeringDBContext, ProjectService>, IProjectServiceRepository
{
    public ProjectServiceRepository(EngineeringDBContext context) : base(context)
    {

    }

    public async Task<ProjectService?> GetProjectServiceById(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.ServiceInfo)
            .Include(oo => oo.ProjectServiceDetails)
                .ThenInclude(oo => oo.ProjectOperationDetailContractorServices)
                    .ThenInclude(oo => oo.ContractorContractDetailServices)

            .Include(oo => oo.ProjectServiceDetails)
                .ThenInclude(oo => oo.ProjectOperationDetailContractorServices)
                    .ThenInclude(oo => oo.DailyOperationServices)
            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectService?> UpdateProjectServiceDoneVolume(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.ProjectServiceDetails)
                .ThenInclude(oo => oo.ProjectOperationDetailContractorServices)
                    .ThenInclude(oo => oo.DailyOperationServices)
            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<GetProjectServiceByIdResponse?> GetProjectServiceByIdNoInclude(
        long id,
        CT ct)
    {
        var query = DbSet

            .Where(oo => oo.Id == id)

            .Select(item => new GetProjectServiceByIdResponse()
            {
                Id = item.Id,
                CostCenterId = item.Project.ProjectCostCenters.Any() ? item.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId : null,
                CostCenterCode = item.Project.ProjectCostCenters.Any() ? item.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterCode : null,
                CostCenterName = item.Project.ProjectCostCenters.Any() ? item.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName : null,
                ProjectId = item.Project.Id,
                ProjectCode = item.Project.ProjectCode,
                ProjectName = item.Project.ProjectName,
                ServiceInfoId = item.ServiceInfo.Id,
                ServiceInfoCode = item.ServiceInfo.ServiceInfoCode,
                ServiceInfoName = item.ServiceInfo.ServiceInfoName,
                ServiceInfoMeasurId = item.ServiceInfo.UnitOfMeasurementId,
                ContractorId = item.ContractorId,
                Volume = item.Volume,
                DoneVolume = item.DoneVolume,
                RemainderVolume = item.RemainderVolume,
                IsActive = item.IsActive,
                Created = item.Created,
                ProjectServiceDetails = item.ProjectServiceDetails.Select(detail => new GetProjectServiceDetailModel()
                {
                    Id = detail.Id,
                    ServiceInfoId = detail.OperationInfoService.ServiceInfo.Id,
                    ServiceInfoCode = detail.OperationInfoService.ServiceInfo.ServiceInfoCode,
                    ServiceInfoName = detail.OperationInfoService.ServiceInfo.ServiceInfoName,
                    ServiceInfoMeasurId = detail.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                    OperationInfoServiceId = detail.OperationInfoService.Id,
                    OperationInfoId = detail.OperationInfoService.OperationInfo.Id,
                    OperationInfoCode = detail.OperationInfoService.OperationInfo.OperationInfoCode,
                    OperationInfoName = detail.OperationInfoService.OperationInfo.OperationInfoName,
                    OperationInfoMeasurId = detail.OperationInfoService.OperationInfo.UnitOfMeasurementId,
                }).ToList()
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<List<ProjectService>> GetsProjectServiceByIds(List<long> ids, CT ct)
    {
        var query = DbSet
            .Where(oo => ids.Contains(oo.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<(List<GetsProjectServiceModel> Data, int RowCount)> GetsProjectService(
        List<long>? ids,
        List<long>? costcenterIds,
        List<long>? projectIds,
        List<long>? serviceInfoIds,
        List<long>? contractorIds,
        bool? isActive,
        string? serviceFilterData,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet

            .Where(oo =>
                (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                 EF.Functions.Like(oo.Project.ProjectName, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(oo.Project.ProjectCode, filterData.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(serviceFilterData) ||
                 EF.Functions.Like(oo.ServiceInfo.ServiceInfoName, serviceFilterData.MakeLikePattern()) ||
                 EF.Functions.Like(oo.ServiceInfo.ServiceInfoCode, serviceFilterData.MakeLikePattern())) &&
                (costcenterIds == null || oo.Project.ProjectCostCenters.Any(x => costcenterIds.Contains(x.CostCenterId))) &&
                (projectIds == null || projectIds.Contains(oo.Project.Id)) &&
                (serviceInfoIds == null || serviceInfoIds.Contains(oo.ServiceInfo.Id)) &&
                (contractorIds == null || contractorIds.Contains(oo.ContractorId)) &&
                (isActive == null || oo.IsActive.Equals(isActive)))

            .Select(x => new GetsProjectServiceModel()
            {
                Id = x.Id,
                ContractorId = x.ContractorId,
                IsActive = x.IsActive,
                CostCenterId = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId : null,
                CostCenterCode = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterCode : null,
                CostCenterName = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName : null,
                ProjectId = x.Project.Id,
                ProjectCode = x.Project.ProjectCode,
                ProjectName = x.Project.ProjectName,
                ServiceInfoCode = x.ServiceInfo.ServiceInfoCode,
                ServiceInfoId = x.ServiceInfo.Id,
                Volume = x.Volume,
                DoneVolume = x.DoneVolume,
                RemainderVolume = x.RemainderVolume,
                ServiceInfoMeasurId = x.ServiceInfo.UnitOfMeasurementId,
                ServiceInfoName = x.ServiceInfo.ServiceInfoName,
                Created = x.Created,
            });

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }


    public async Task<(List<GetsActiveProjectServiceModel> Data, int RowCount)> GetActiveProjectServices(
        List<long>? ids,
        List<long>? costcenterIds,
        List<long>? projectIds,
        List<long>? serviceInfoIds,
        List<long>? contractorIds,
        string? serviceFilterData,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(oo =>
                oo.IsActive == true &&
                (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                 EF.Functions.Like(oo.Project.ProjectName, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(oo.Project.ProjectCode, filterData.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(serviceFilterData) ||
                 EF.Functions.Like(oo.ServiceInfo.ServiceInfoName, serviceFilterData.MakeLikePattern()) ||
                 EF.Functions.Like(oo.ServiceInfo.ServiceInfoCode, serviceFilterData.MakeLikePattern())) &&
                (costcenterIds == null || oo.Project.ProjectCostCenters.Any(x => costcenterIds.Contains(x.CostCenterId))) &&
                (projectIds == null || projectIds.Contains(oo.Project.Id)) &&
                (serviceInfoIds == null || serviceInfoIds.Contains(oo.ServiceInfo.Id)) &&
                (contractorIds == null || contractorIds.Contains(oo.ContractorId)))
            .Select(x => new GetsActiveProjectServiceModel()
            {
                Id = x.Id,
                ContractorId = x.ContractorId,
                CostCenterId = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId : null,
                CostCenterCode = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterCode : null,
                CostCenterTitle = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName : null,
                ProjectId = x.Project.Id,
                ProjectCode = x.Project.ProjectCode,
                ProjectTitle = x.Project.ProjectName,
                ServiceInfoCode = x.ServiceInfo.ServiceInfoCode,
                ServiceInfoId = x.ServiceInfo.Id,
                Volume = x.Volume,
                DoneVolume = x.DoneVolume,
                RemainderVolume = x.RemainderVolume,
                ServiceInfoMeasurId = x.ServiceInfo.UnitOfMeasurementId,
                ServiceInfoTitle = x.ServiceInfo.ServiceInfoName,
                Created = x.Created,
            });

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsContractorServiceModel> Data, int RowCount)> GetProjectServices(
       long projectId,
       string? filterData,
       int pageIndex,
       int pageSize,
       CT ct)
    {
        var query = DbSet
            .Include(oo => oo.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
            .Where(oo =>
                (string.IsNullOrWhiteSpace(filterData) ||
                 EF.Functions.Like(oo.ServiceInfo.ServiceInfoName, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(oo.ServiceInfo.ServiceInfoCode, filterData.MakeLikePattern())) &&
                 oo.Project.Id.Equals(projectId))
            .Select(x => new GetsContractorServiceModel()
            {
                Id = x.Id,
                ContractorId = x.ContractorId,
                ServiceInfoCode = x.ServiceInfo.ServiceInfoCode,
                ServiceInfoId = x.ServiceInfo.Id,
                ServiceInfoMeasurId = x.ServiceInfo.UnitOfMeasurementId,
                ServiceInfoTitle = x.ServiceInfo.ServiceInfoName,
                Volume = x.Volume,
                DoneVolume = x.DoneVolume,
                RemainderVolume = x.RemainderVolume,
                Created = x.Created,
                IsActive = x.IsActive,
            });

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<long>?> GetServiceContractors(
        long? projectId,
        List<long> serviceInfoIds,
        CT ct)
    {
        var query = DbSet
            .Where(x => x.Project.Id.Equals(projectId))
            .GroupBy(x => x.ContractorId)
            .Where(x => serviceInfoIds.All(s => x.Any(c => c.ServiceInfo.Id == s)))
            .Select(contractor => contractor.Key);

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<bool> IsDuplicateProjectService(
        long? id,
        long projectId,
        long serviceInfoId,
        long contractorId,
        CT ct) => await DbSet.AnyAsync(x =>
            x.Project.Id == projectId &&
            x.ServiceInfo.Id == serviceInfoId &&
            x.ContractorId == contractorId &&
            (id == null || x.Id != id));
}