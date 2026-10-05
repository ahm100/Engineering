using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.HaveProjectChild;

public record HaveProjectChildQuery(
    long Id
    ) : IQuery<Project>;