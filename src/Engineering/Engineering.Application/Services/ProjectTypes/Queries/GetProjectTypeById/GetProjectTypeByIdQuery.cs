using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Queries.GetProjectTypeById;

public record GetProjectTypeByIdQuery(
    long Id
    ) : IQuery<ProjectType>;