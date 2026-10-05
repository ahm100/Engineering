using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetProjectById;

public record GetProjectByIdQuery(
    long Id) : IQuery<Project>;