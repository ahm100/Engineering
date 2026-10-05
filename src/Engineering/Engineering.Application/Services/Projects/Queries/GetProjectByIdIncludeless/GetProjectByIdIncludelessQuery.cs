using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetProjectByIdIncludeless;

public record GetProjectByIdIncludelessQuery(
    long Id
    ) : IQuery<Project>;