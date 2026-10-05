using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.Projects.Queries.GetProjectByIdIncludeless;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectRisks.Commands.CreateProjectRisk;

public class CreateProjectRiskCommandHandler : ICommandHandler<CreateProjectRiskCommand, ProjectRisk>
{
    private readonly ILogger<CreateProjectRiskCommandHandler> _logger;
    private readonly IProjectRiskRepository _repository;
    private readonly IMediator _mediator;

    public CreateProjectRiskCommandHandler(ILogger<CreateProjectRiskCommandHandler> logger,
        IProjectRiskRepository repository,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Result<ProjectRisk?>> Handle(CreateProjectRiskCommand request, CT ct)
    {
        try
        {
            var project = await _mediator.Send(new GetProjectByIdIncludelessQuery(request.ProjectId), ct);
            if (project.IsBad())
                return project.Failure<ProjectRisk>()!;

            var create = new ProjectRisk(project.Value!,
                request.Code,
                request.Title,
                request.RiskProbability,
                request.RiskImpact,
                request.RiskStatus,
                request.IsActive);

            await _repository.Create(create, ct);

            return create;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectRisk>(SharedErrors.UnknownError);
        }
    }
}