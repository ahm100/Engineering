using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditTaskActualDateTimes;

public class EditTaskActualDateTimesCommandHandler : ICommandHandler<EditTaskActualDateTimesCommand, ProjectScheduleTask?>
{
    private readonly ILogger<EditTaskActualDateTimesCommandHandler> _logger;
    private readonly IProjectScheduleTaskRepository _repository;

    public EditTaskActualDateTimesCommandHandler(
        ILogger<EditTaskActualDateTimesCommandHandler> logger,
        IProjectScheduleTaskRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectScheduleTask?>> Handle(
        EditTaskActualDateTimesCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.TaskId, ct);
            if (entity is null)
                return Result.Failure<ProjectScheduleTask>(ProjectErrors.ProjectTaskNotFound)!;

            if (request.IsStart)
                entity.SetActualStart(request.DateTime);
            else
                entity.SetActualFinish(request.DateTime);

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