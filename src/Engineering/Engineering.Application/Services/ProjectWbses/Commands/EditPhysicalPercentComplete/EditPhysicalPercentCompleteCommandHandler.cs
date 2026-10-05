using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditPhysicalPercentComplete;

public class EditPhysicalPercentCompleteCommandHandler : ICommandHandler<EditPhysicalPercentCompleteCommand, ProjectScheduleTask?>
{
    private readonly ILogger<EditPhysicalPercentCompleteCommandHandler> _logger;
    private readonly IProjectScheduleTaskRepository _repository;
    public EditPhysicalPercentCompleteCommandHandler(
        ILogger<EditPhysicalPercentCompleteCommandHandler> logger,
        IProjectScheduleTaskRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectScheduleTask?>> Handle(
        EditPhysicalPercentCompleteCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectScheduleTask>(ProjectErrors.ProjectTaskNotFound)!;

            entity.SetPhysicalPercentComplete(request.Percent ?? 0);
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
