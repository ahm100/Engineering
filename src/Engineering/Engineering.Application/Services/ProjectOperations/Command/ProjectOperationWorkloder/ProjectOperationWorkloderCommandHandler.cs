using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperations.Commands.ProjectOperationWorkloder;

public class ProjectOperationWorkloderCommandHandler : ICommandHandler<ProjectOperationWorkloderCommand, ProjectOperation>
{

    private readonly ILogger<ProjectOperationWorkloderCommand> _logger;
    private readonly IProjectOperationRepository _repository;

    public ProjectOperationWorkloderCommandHandler(
        ILogger<ProjectOperationWorkloderCommand> logger,
        IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(ProjectOperationWorkloderCommand request, CT ct)
    {
        try
        {
            var projectOperation = await _repository.ProjectOperationWorkloder(request.Id, ct);
            if (projectOperation is null)
                return Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);

            decimal newWorkload = request.ProjectOperationDetails.Where(x => x.IsNew).Sum(x => x.FinalAmount);
            if (request.ProjectOperationDetails is not null)
                if (projectOperation.ProjectOperationDetailLists is not null)
                    foreach (var item in projectOperation.ProjectOperationDetailLists)
                    {
                        var detail = request.ProjectOperationDetails.Where(x => !x.IsNew).FirstOrDefault(x => x.Id.Equals(item.Id));
                        if (detail is not null)
                            item.FinalAmount = detail.FinalAmount;
                    }

            var workload = projectOperation.ProjectOperationDetailLists!.Sum(x => x.FinalAmount) + newWorkload;
            var entity = await _repository.GetProjectOperationByIdOperationInfoInclude(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);

            if (!entity.Project.Contractual)
            {
                if (workload > entity.Workload)
                    entity.SetWorkload(entity.Workload);
                else
                    entity.SetWorkload(workload);
            }
            else
                entity.SetWorkload(workload);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}