using Engineering.Application.Services.ProjectCostCenterRequests.Models;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Projects.ProjectCostCenterRequests;

namespace Engineering.Application.Abstractions.Data.ProjectCostCenterRequests;

public interface IProjectCostCenterRequestRepository : IBaseRepository<ProjectCostCenterRequest>
{
    Task<ProjectCostCenterRequest?> GetById(long id, CT ct);
    Task<List<ProjectCostCenterRequest>> GetByProjectId(long projectId, CT ct);
    Task<List<ProjectCostCenterRequest>> GetPendingByProjectId(long projectId, CT ct);

    Task<(List<ProjectCostCenterRequestModel> Data, int RowCount)> GetRequests(
        long? projectId,
        ProjectCostCenterRequestStatus? status,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<ProjectCostCenterRequestModel?> GetModelById(long id, CT ct);
}