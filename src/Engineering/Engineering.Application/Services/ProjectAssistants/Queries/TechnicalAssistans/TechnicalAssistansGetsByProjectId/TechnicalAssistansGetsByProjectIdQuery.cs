using ProjectTechnicalAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectTechnicalAssistant;

namespace Engineering.Application.Services.ProjectAssistants.Queries.TechnicalAssistans.TechnicalAssistansGetsByProjectId;

public record TechnicalAssistansGetsByProjectIdQuery(
    long ProjectId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectTechnicalAssistant>>>;