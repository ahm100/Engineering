using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetProjectByIdNoIncluding;

public record GetProjectByIdNoIncludingQuery(
    long Id
    ) : IQuery<Project>;