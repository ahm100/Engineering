using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetSummarizedProjectById;

public record GetSummarizedProjectByIdQuery(
    long Id
    ) : IQuery<Project>;