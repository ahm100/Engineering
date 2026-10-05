using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.Delete;

public class DeleteProjectOperationCommandHandler : ICommandHandler<DeleteProjectOperationCommand, ProjectOperation>
{
    private readonly ILogger<DeleteProjectOperationCommand> _logger;
    private readonly IProjectOperationRepository _repository;

    public DeleteProjectOperationCommandHandler(ILogger<DeleteProjectOperationCommand> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(DeleteProjectOperationCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<ProjectOperation>(ProjectOperationErrors.IsDeleted);
            if (entity.ProjectOperationDetails?.Count > 0)
                return Result.Failure<ProjectOperation>(ProjectOperationErrors.CanNottDeleteForProjectOperationDetail);
            if (entity.SuccessorProjectOperationDependencies.Any())
                return Result.Failure<ProjectOperation>(ProjectOperationErrors.CanNottDelete);

            entity.SetIsDeleted();

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