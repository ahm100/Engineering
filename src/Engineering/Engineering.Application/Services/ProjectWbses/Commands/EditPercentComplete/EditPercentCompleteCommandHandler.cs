using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditPercentComplete;

public class EditPercentCompleteCommandHandler : ICommandHandler<EditPercentCompleteCommand, ProjectScheduleTask?>
{
    private readonly ILogger<EditPercentCompleteCommandHandler> _logger;
    private readonly IProjectScheduleTaskRepository _repository;

    public EditPercentCompleteCommandHandler(
        ILogger<EditPercentCompleteCommandHandler> logger,
        IProjectScheduleTaskRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectScheduleTask?>> Handle(
        EditPercentCompleteCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);

            if (entity is null)
                return Result.Failure<ProjectScheduleTask>(ProjectErrors.ProjectTaskNotFound)!;

            entity.SetPercentComplete(request.Percent ?? 0);
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectScheduleTask>(SharedErrors.UnknownError)!;
        }
    }
}
