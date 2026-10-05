using ProjectImplementationAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectImplementationAssistant;

namespace Engineering.Application.Abstractions.Data.Projects;

public interface IProjectImplementationAssistantRepository : IBaseRepository<ProjectImplementationAssistant>
{
    Task<ProjectImplementationAssistant?> FindUser(long userId, long projectId, CT ct);
    Task<(List<ProjectImplementationAssistant> Data, int RowCount)> GetImplementationAssistansByProjectId(long projectId, int pageIndex, int pageSize, CT ct);
}