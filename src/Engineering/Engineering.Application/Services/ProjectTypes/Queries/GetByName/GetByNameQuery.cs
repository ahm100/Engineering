using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectTypes.Queries.GetByName;

public record GetProjectTypeByNameQuery(
    string ProjectTypeName,
    long? CompanyId
    ) : IQuery<ProjectType?>;
