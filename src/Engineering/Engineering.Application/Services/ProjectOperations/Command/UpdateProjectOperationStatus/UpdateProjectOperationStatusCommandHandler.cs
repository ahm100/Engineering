using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperationStatus;

public class UpdateProjectOperationStatusCommandHandler : ICommandHandler<UpdateProjectOperationStatusCommand, ProjectOperation>
{
    private readonly ILogger<UpdateProjectOperationStatusCommand> _logger;
    private readonly IProjectOperationRepository _repository;
    private readonly IProjectOperationDependencyRepository _projectOperationDependencyRepository;

    public UpdateProjectOperationStatusCommandHandler(ILogger<UpdateProjectOperationStatusCommand> logger, IProjectOperationRepository repository,
        IProjectOperationDependencyRepository projectOperationDependencyRepository)
    {
        _logger = logger;
        _repository = repository;
        _projectOperationDependencyRepository = projectOperationDependencyRepository;
    }

    public async Task<Result<ProjectOperation?>> Handle(UpdateProjectOperationStatusCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.ProjectOperationWorkloadManagement(request.ProjectOperationId, ct);
            if (entity is null)
                return Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);

            var projectOperationDetails = entity.ProjectOperationDetails.Where(x => !x.IsDeleted).ToList();

            if (projectOperationDetails is not null && projectOperationDetails.Count > 0)
            {
                if (projectOperationDetails.Any(x => x.Status == ProjectOperationDetailStatus.Doing))
                {
                    entity.SetProjectOperationStatus(ProjectOperationStatus.Doing);
                }
                else if (projectOperationDetails.All(x => x.Status == ProjectOperationDetailStatus.NotStarted))
                {
                    entity.SetProjectOperationStatus(ProjectOperationStatus.NotStarted);
                }
                else if (projectOperationDetails.All(x => x.Status == ProjectOperationDetailStatus.Stopped))
                {
                    entity.SetProjectOperationStatus(ProjectOperationStatus.Stopped);
                }
                else if (projectOperationDetails.All(x => x.Status == ProjectOperationDetailStatus.EndOfWork))
                {
                    entity.SetProjectOperationStatus(ProjectOperationStatus.EndOfWork);
                }
                else if (projectOperationDetails.All(x => x.Status == ProjectOperationDetailStatus.TemporaryDelivery))
                {
                    entity.SetProjectOperationStatus(ProjectOperationStatus.TemporaryDelivery);
                }
                else if (projectOperationDetails.All(x => x.Status == ProjectOperationDetailStatus.DefiniteDelivery))
                {
                    entity.SetProjectOperationStatus(ProjectOperationStatus.DefiniteDelivery);
                }
                else
                {
                    entity.SetProjectOperationStatus(ProjectOperationStatus.Doing);
                }
            }

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}