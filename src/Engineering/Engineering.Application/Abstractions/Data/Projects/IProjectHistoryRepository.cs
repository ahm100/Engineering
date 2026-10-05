using Engineering.Application.Services.Projects.Models.GetProjectHistory;
using Engineering.Domain.Entities.Projects.Histories;

namespace Engineering.Application.Abstractions.Data.Projects;

public interface IProjectHistoryRepository : IBaseRepository<ProjectHistory>
{
    Task<List<GetProjectHistoryModel>?> GetProjectHistory(
        long id,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);
}