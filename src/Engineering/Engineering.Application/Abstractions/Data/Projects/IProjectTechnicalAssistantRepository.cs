using Engineering.Domain.Entities.Projects.ProjectUsers;

namespace Engineering.Application.Abstractions.Data.Projects;

public interface IProjectTechnicalAssistantRepository : IBaseRepository<ProjectTechnicalAssistant>
{
    Task<ProjectTechnicalAssistant?> FindUser(long userId, long projectId, CT ct);
    Task<(List<ProjectTechnicalAssistant> Data, int RowCount)> GetImpProjectTechnicalAssistantByProjectId(long projectId, int pageIndex, int pageSize, CT ct);
}