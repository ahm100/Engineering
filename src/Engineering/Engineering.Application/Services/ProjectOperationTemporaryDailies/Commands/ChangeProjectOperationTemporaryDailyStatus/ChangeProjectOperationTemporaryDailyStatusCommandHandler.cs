using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Commands.ChangeProjectOperationTemporaryDailyStatus;

public class ChangeProjectOperationTemporaryDailyStatusCommandHandler : ICommandHandler<ChangeProjectOperationTemporaryDailyStatusCommand, ProjectOperationTemporaryDaily>
{
    private readonly ILogger<ChangeProjectOperationTemporaryDailyStatusCommandHandler> _logger;
    private readonly IProjectOperationTemporaryDailyRepository _repository;

    public ChangeProjectOperationTemporaryDailyStatusCommandHandler(ILogger<ChangeProjectOperationTemporaryDailyStatusCommandHandler> logger, IProjectOperationTemporaryDailyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationTemporaryDaily?>> Handle(ChangeProjectOperationTemporaryDailyStatusCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;

            entity.ChangeStatus(request.Status);

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
