using Engineering.Application.Services.ProjectServices.Models.GetProjectServiceById;
using Engineering.Application.Services.ProjectServices.Models.GetsProjectService;
using Engineering.Application.Services.ProjectServices.Models.ProjectServiceModels;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Abstractions.Data.Projects;

public interface IProjectServiceRepository : IBaseRepository<ProjectService>
{
    Task<ProjectService?> GetProjectServiceById(
        long id,
        CT ct);

    Task<ProjectService?> UpdateProjectServiceDoneVolume(
        long id,
        CT ct);

    Task<GetProjectServiceByIdResponse?> GetProjectServiceByIdNoInclude(
        long id,
        CT ct);

    Task<List<ProjectService>> GetsProjectServiceByIds(
        List<long> ids,
        CT ct);

    Task<(List<GetsProjectServiceModel> Data, int RowCount)> GetsProjectService(
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
        CT ct);

    Task<(List<GetsActiveProjectServiceModel> Data, int RowCount)> GetActiveProjectServices(
        List<long>? ids,
        List<long>? costcenterIds,
        List<long>? projectIds,
        List<long>? serviceInfoIds,
        List<long>? contractorIds,
        string? serviceFilterData,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsContractorServiceModel> Data, int RowCount)> GetProjectServices(
        long projectId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<long>?> GetServiceContractors(
        long? projectId,
        List<long> serviceInfoIds,
        CT ct);

    Task<bool> IsDuplicateProjectService(long? id, long projectId, long serviceInfoId, long contractorId, CT ct);
}