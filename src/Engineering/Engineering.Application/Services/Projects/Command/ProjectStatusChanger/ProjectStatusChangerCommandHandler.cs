using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Commands.ProjectStatusChanger;

public class ProjectStatusChangerCommandHandler : ICommandHandler<ProjectStatusChangerCommand, Project>
{
    private readonly ILogger<ProjectStatusChangerCommand> _logger;
    private readonly IProjectRepository _repository;

    public ProjectStatusChangerCommandHandler(ILogger<ProjectStatusChangerCommand> logger, IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Project?>> Handle(ProjectStatusChangerCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetForChangeStatus(request.Id, ct);
            if (entity is null)
                return Result.Failure<Project>(ProjectErrors.ProjectWithIdNotFound);

            switch (request.Status)
            {
                case ProjectStatus.NotStarted:
                    if (entity.Status != ProjectStatus.NotStarted)
                        return Result.Failure<Project>(ProjectErrors.CanNotChangeStatus);
                    break;
                case ProjectStatus.Doing:

                    break;
                case ProjectStatus.Stopped:

                    break;
                case ProjectStatus.EndOfWork:

                    break;
                case ProjectStatus.TemporaryDelivery:

                    break;
                case ProjectStatus.DefiniteDelivery:

                    break;
                case ProjectStatus.Closed:

                    break;
                case ProjectStatus.Canceled:

                    break;
            }

            if (request.ProjectOperationDetailId is not null && request.ProjectOperationDetailId > 0)
            {
                entity.ProjectOperations.Where(x => x.Id == request.ProjectOperationId).FirstOrDefault()?.ProjectOperationDetails
                    .Where(x => x.Id == request.ProjectOperationDetailId).FirstOrDefault()?.SetStatus((ProjectOperationDetailStatus)request.Status);

                entity.ProjectOperations.Where(x => x.Id == request.ProjectOperationId).FirstOrDefault()?.ProjectOperationDetails
                       .Where(x => x.Id == request.ProjectOperationDetailId).FirstOrDefault()?.AddHistory(request.StatusDescription);
            }
            if (request.ProjectOperationId is not null && request.ProjectOperationId > 0)
            {
                if ((bool)entity.ProjectOperations.Where(x => x.Id == request.ProjectOperationId)?.FirstOrDefault()?.ProjectOperationDetails.All(x => x.Status == (ProjectOperationDetailStatus)request.Status)!)
                    entity.ProjectOperations.Where(x => x.Id == request.ProjectOperationId).FirstOrDefault()?.SetProjectOperationStatus((ProjectOperationStatus)request.Status);
                else
                    entity.ProjectOperations.Where(x => x.Id == request.ProjectOperationId).FirstOrDefault()?.SetProjectOperationStatus(ProjectOperationStatus.Doing);
            }

            var status = entity.StatusChecker(entity);
            entity.SetStatus(status);
            entity.AddHistory();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Project>(SharedErrors.UnknownError);
        }
    }
}