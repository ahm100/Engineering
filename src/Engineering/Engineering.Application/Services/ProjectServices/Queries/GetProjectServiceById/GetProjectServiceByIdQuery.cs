using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Queries.GetProjectServiceById;

public record GetProjectServiceByIdQuery(
    long Id
    ) : IQuery<ProjectService>;