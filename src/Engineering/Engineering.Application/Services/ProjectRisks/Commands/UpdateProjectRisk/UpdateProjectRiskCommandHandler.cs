using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.Projects.Queries.GetProjectByIdIncludeless;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Errors.Projects;

namespace Engineering.Application.Services.ProjectRisks.Commands.UpdateProjectRisk;

public class UpdateProjectRiskCommandHandler : ICommandHandler<UpdateProjectRiskCommand, ProjectRisk?>
{
    private readonly ILogger<UpdateProjectRiskCommandHandler> _logger;
    private readonly IProjectRiskRepository _repository;
    private readonly IMediator _mediator;

    public UpdateProjectRiskCommandHandler(ILogger<UpdateProjectRiskCommandHandler> logger,
        IProjectRiskRepository repository,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Result<ProjectRisk?>> Handle(UpdateProjectRiskCommand request, CT ct)
    {
        try
        {
            var projectRisk = await _repository.GetById(request.Id, ct);
            if (projectRisk is null)
                return Result.Failure<ProjectRisk>(ProjectRiskErrors.ProjectRiskWithIdNotFound);

            Project? project = null;
            if (request.ProjectId is not null)
            {
                var result = await _mediator.Send(new GetProjectByIdIncludelessQuery(request.ProjectId.Value), ct);
                project = !result.IsBad() ? result.Value : null;
            }

            projectRisk.Update(project,
                request.Code,
                request.Title,
                request.RiskProbability,
                request.RiskImpact,
                request.RiskStatus,
                request.IsActive);

            await _repository.Update(projectRisk);

            return projectRisk;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectRisk>(SharedErrors.UnknownError);
        }
    }
}