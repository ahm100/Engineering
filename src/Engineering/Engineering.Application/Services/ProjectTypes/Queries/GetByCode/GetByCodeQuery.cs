using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectTypes.Queries.GetByCode;

public record GetProjectTypeByCodeQuery(
    string ProjectTypeCode,
    long? CompanyId
    ) : IQuery<ProjectType?>;