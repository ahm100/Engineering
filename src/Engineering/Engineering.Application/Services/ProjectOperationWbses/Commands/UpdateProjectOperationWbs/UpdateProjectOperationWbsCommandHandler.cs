using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdIncludelessNew;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects.WBS;
using Engineering.Domain.Errors.WbsTemplates;

namespace Engineering.Application.Services.ProjectOperationWbses.Commands.UpdateProjectOperationWbs;

public class UpdateProjectOperationWbsCommandHandler : ICommandHandler<UpdateProjectOperationWbsCommand, ProjectOperationWbs?>
{
    private readonly ILogger<UpdateProjectOperationWbsCommandHandler> _logger;
    private readonly IProjectOperationWbsRepository _repository;
    private readonly IProjectWbsRepository _projectWbsRepo;
    private readonly IMediator _mediator;

    public UpdateProjectOperationWbsCommandHandler(ILogger<UpdateProjectOperationWbsCommandHandler> logger,
        IProjectOperationWbsRepository repository,
        IProjectWbsRepository projectWbsRepo,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _mediator = mediator;
        _projectWbsRepo = projectWbsRepo;
    }

    public async Task<Result<ProjectOperationWbs?>> Handle(UpdateProjectOperationWbsCommand request, CT ct)
    {
        try
        {
            var pOWbs = await _repository.GetById(request.Id, ct);
            if (pOWbs is null)
                return Result.Failure<ProjectOperationWbs?>(WbsTemplateErrors.ProjectOperationWbsWithIdNotFound);
            ProjectOperation? projectOperation = null;
            if (request.ProjectOperationId is not null)
            {
                var projectOperationData = await _mediator.Send(new GetProjectOperationByIdIncludelessNewQuery(request.ProjectOperationId.Value), ct);
                if (projectOperationData.IsBad())
                    return projectOperationData.Failure<ProjectOperationWbs?>();

                projectOperation = projectOperationData.Value;
            }

            ProjectWbs? projectWbs = null;
            if (request.ProjectWbsId is not null)
            {
                var projectWbsData = await _projectWbsRepo.GetById(request.ProjectWbsId.Value, ct);
                if (projectWbsData is null)
                    return Result.Failure<ProjectOperationWbs?>(WbsTemplateErrors.ProjectWbsWithIdNotFound);
                projectWbs = projectWbsData;
            }

            pOWbs.Update(projectOperation, projectWbs, request.IsActive);

            return pOWbs;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationWbs?>(SharedErrors.UnknownError);
        }
    }
}
