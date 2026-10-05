using Engineering.Application.Services.ProjectServices.Models.GetsProjectServiceDetail;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Abstractions.Data.Projects;

public interface IProjectServiceDetailRepository : IBaseRepository<ProjectServiceDetail>
{
    Task<ProjectServiceDetail?> GetProjectServiceDetailById(long id, CT ct);

    Task<(List<GetsProjectServiceDetailModel> Data, int RowCount)> GetsProjectServiceDetail(
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
        CT ct);

    Task<(List<ProjectServiceDetail> Data, int RowCount)> GetsProjectServiceDetailByIds(
        List<long> ids,
        int pageIndex,
        int pageSize,
        CT ct);

}