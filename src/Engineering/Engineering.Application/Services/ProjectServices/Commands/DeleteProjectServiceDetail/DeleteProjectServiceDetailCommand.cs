using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectServices.Commands.DeleteProjectServiceDetail;

public record DeleteProjectServiceDetailCommand(
    long Id
    ) : ICommand<ProjectServiceDetail>;