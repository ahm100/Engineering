using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectRisks.Commands.DeleteProjectRisk;

public record DeleteProjectRiskCommand(
    long Id
    ) : ICommand<ProjectRisk>;