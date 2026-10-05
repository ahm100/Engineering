using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Errors.Projects;

namespace Engineering.Application.Services.ProjectRisks.Commands.DeleteProjectRisk;

public class DeleteProjectRiskCommandHandler : ICommandHandler<DeleteProjectRiskCommand, ProjectRisk>
{
    private readonly ILogger<DeleteProjectRiskCommandHandler> _logger;
    private readonly IProjectRiskRepository _repository;
    private readonly IMediator _mediator;

    public DeleteProjectRiskCommandHandler(ILogger<DeleteProjectRiskCommandHandler> logger,
        IProjectRiskRepository repository,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Result<ProjectRisk?>> Handle(DeleteProjectRiskCommand request, CT ct)
    {
        try
        {
            var projectRisk = await _repository.GetById(request.Id, ct);
            if (projectRisk is null)
                return Result.Failure<ProjectRisk>(ProjectRiskErrors.ProjectRiskWithIdNotFound);

            projectRisk.SoftDelete();

            return projectRisk;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectRisk>(SharedErrors.UnknownError);
        }
    }
}