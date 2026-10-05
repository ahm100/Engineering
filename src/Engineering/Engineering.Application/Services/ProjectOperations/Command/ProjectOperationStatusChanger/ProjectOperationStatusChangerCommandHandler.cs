using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Commands.ProjectOperationStatusChanger;

public class ProjectOperationStatusChangerCommandHandler : ICommandHandler<ProjectOperationStatusChangerCommand, ProjectOperation>
{
    private readonly ILogger<ProjectOperationStatusChangerCommand> _logger;
    private readonly IProjectOperationRepository _repository;

    public ProjectOperationStatusChangerCommandHandler(ILogger<ProjectOperationStatusChangerCommand> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(ProjectOperationStatusChangerCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);

            switch (request.Status)
            {
                case ProjectOperationStatus.NotStarted:
                    if (entity.ProjectOperationStatus != ProjectOperationStatus.NotStarted)
                        return Result.Failure<ProjectOperation>(ProjectOperationErrors.CanNottChanegToNotStarted);
                    break;
                case ProjectOperationStatus.Doing:

                    break;
                case ProjectOperationStatus.Stopped:

                    break;
                case ProjectOperationStatus.EndOfWork:

                    break;
                case ProjectOperationStatus.TemporaryDelivery:

                    break;
                case ProjectOperationStatus.DefiniteDelivery:

                    break;
            }

            entity.SetProjectOperationStatus(request.Status);
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