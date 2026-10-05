using ProjectImplementationAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectImplementationAssistant;

namespace Engineering.Application.Services.ProjectAssistants.Queries.ImplementationAssistans.ImplementationAssistansGetsByProjectId;

public record ImplementationAssistansGetsByProjectIdQuery(
    long ProjectId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectImplementationAssistant>>>;