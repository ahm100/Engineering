using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationByIdsIncludeless;
using Engineering.Domain.Entities.Projects.WBS;
using Engineering.Domain.Errors.WbsTemplates;

namespace Engineering.Application.Services.ProjectOperationWbses.Commands.CreateProjectOperationWbs;

public class CreateProjectOperationWbsCommandHandler : ICommandHandler<CreateProjectOperationWbsCommand, List<ProjectOperationWbs>?>
{
    private readonly ILogger<CreateProjectOperationWbsCommandHandler> _logger;
    private readonly IProjectOperationWbsRepository _repository;
    private readonly IProjectWbsRepository _projectWbsRepo;
    private readonly IMediator _mediator;

    public CreateProjectOperationWbsCommandHandler(ILogger<CreateProjectOperationWbsCommandHandler> logger,
        IProjectOperationWbsRepository repository,
        IProjectWbsRepository projectWbsRepo,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _mediator = mediator;
        _projectWbsRepo = projectWbsRepo;
    }

    public async Task<Result<List<ProjectOperationWbs>?>> Handle(CreateProjectOperationWbsCommand request, CT ct)
    {
        try
        {
            var projectOperations = await _mediator.Send(new GetsProjectOperationByIdsIncludelessQuery(request.ProjectOperationIds, null, null, 0, 0), ct);
            if (projectOperations.IsBad() || projectOperations.Value!.Data == null)
                return projectOperations.Failure<List<ProjectOperationWbs>?>();

            var projectWbs = await _projectWbsRepo.GetById(request.ProjectWbsId, ct);
            if (projectWbs is null)
                return Result.Failure<List<ProjectOperationWbs>?>(WbsTemplateErrors.ProjectWbsWithIdNotFound);

            List<ProjectOperationWbs>? result = [];
            foreach (var item in projectOperations.Value.Data)
            {
                var entity = new ProjectOperationWbs(item,
                    projectWbs,
                    request.IsActive);

                var create = await _repository.Create(entity, ct);
                result.Add(create);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectOperationWbs>?>(SharedErrors.UnknownError);
        }
    }
}
