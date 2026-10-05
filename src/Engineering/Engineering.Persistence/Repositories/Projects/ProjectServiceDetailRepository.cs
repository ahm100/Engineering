using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.ProjectServices.Models.GetsProjectServiceDetail;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Repositories.Projects;

public class ProjectServiceDetailRepository : BaseRepository<EngineeringDBContext, ProjectServiceDetail>, IProjectServiceDetailRepository
{
    public ProjectServiceDetailRepository(EngineeringDBContext context) : base(context)
    {

    }

    public async Task<ProjectServiceDetail?> GetProjectServiceDetailById(
        long id,
        CT ct)
    {
        var query = DbSet

            .Include(oo => oo.ProjectOperationDetailContractorServices)
                .ThenInclude(oo => oo.ContractorContractDetailServices)

            .Include(oo => oo.ProjectOperationDetailContractorServices)
                .ThenInclude(oo => oo.DailyOperationServices)

            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<GetsProjectServiceDetailModel> Data, int RowCount)> GetsProjectServiceDetail(
        List<long>? ids,
        List<long>? costcenterIds,
        List<long>? projectIds,
        List<long>? serviceInfoIds,
        List<long>? contractorIds,
        List<long>? projectOperationIds,
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
                 EF.Functions.Like(oo.ProjectService.Project.ProjectName, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(oo.ProjectService.Project.ProjectCode, filterData.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(serviceFilterData) ||
                 EF.Functions.Like(oo.ProjectService.ServiceInfo.ServiceInfoName, serviceFilterData.MakeLikePattern()) ||
                 EF.Functions.Like(oo.ProjectService.ServiceInfo.ServiceInfoCode, serviceFilterData.MakeLikePattern())) &&
                (costcenterIds == null || oo.ProjectService.Project.ProjectCostCenters.Any(x => costcenterIds.Contains(x.CostCenterId))) &&
                (projectIds == null || projectIds.Contains(oo.ProjectService.Project.Id)) &&
                (serviceInfoIds == null || serviceInfoIds.Contains(oo.ProjectService.ServiceInfo.Id)) &&
                (contractorIds == null || contractorIds.Contains(oo.ProjectService.ContractorId)) &&
                (projectOperationIds == null || oo.OperationInfoService.OperationInfo.ProjectOperations.Any(p => projectOperationIds.Contains(p.Id))) &&
                (isActive == null || oo.ProjectService.IsActive.Equals(isActive)))

            .Select(x => new GetsProjectServiceDetailModel()
            {
                Id = x.Id,
                ContractorId = x.ProjectService.ContractorId,
                ServiceInfoId = x.ProjectService.ServiceInfo.Id,
                ServiceInfoName = x.OperationInfoService.ServiceInfo.ServiceInfoName,
                ServiceInfoCode = x.OperationInfoService.ServiceInfo.ServiceInfoCode,
                ServiceInfoMeasurId = x.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                OperationInfoServiceId = x.OperationInfoService.Id,
                OperationInfoId = x.OperationInfoService.OperationInfo.Id,
                OperationInfoCode = x.OperationInfoService.OperationInfo.OperationInfoCode,
                OperationInfoName = x.OperationInfoService.OperationInfo.OperationInfoName,
                OperationInfoMeasurId = x.OperationInfoService.OperationInfo.UnitOfMeasurementId,

                ProjectServiceInfoId = x.OperationInfoService.ServiceInfo.Id,
                ProjectServiceInfoCode = x.ProjectService.ServiceInfo.ServiceInfoCode,
                ProjectServiceInfoName = x.ProjectService.ServiceInfo.ServiceInfoName,
                ProjectServiceInfoMeasurId = x.ProjectService.ServiceInfo.UnitOfMeasurementId,
                ProjectServiceVolume = x.ProjectService.Volume,
                ProjectServiceDoneVolume = x.ProjectService.DoneVolume,
            });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectServiceDetail> Data, int RowCount)> GetsProjectServiceDetailByIds(
        List<long> ids,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet

            .Include(x => x.ProjectService)
            .Include(x => x.OperationInfoService)

            .Where(oo => ids.Contains(oo.Id));

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

}