using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Commands.DeleteProjectOperationTemporaryDaily;

public class DeleteProjectOperationTemporaryDailyCommandHandler : ICommandHandler<DeleteProjectOperationTemporaryDailyCommand, ProjectOperationTemporaryDaily>
{
    private readonly ILogger<DeleteProjectOperationTemporaryDailyCommandHandler> _logger;
    private readonly IProjectOperationTemporaryDailyRepository _repository;

    public DeleteProjectOperationTemporaryDailyCommandHandler(ILogger<DeleteProjectOperationTemporaryDailyCommandHandler> logger, IProjectOperationTemporaryDailyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationTemporaryDaily?>> Handle(DeleteProjectOperationTemporaryDailyCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.ProjectOperationTemporaryDailyId, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationTemporaryDaily>(ProjectOperationTemporaryDailyErrors.TemporaryDailyWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<ProjectOperationTemporaryDaily>(ProjectOperationTemporaryDailyErrors.IsDeleted);

            entity.SetIsDeleted();

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationTemporaryDaily>(SharedErrors.UnknownError);
        }
    }
}
