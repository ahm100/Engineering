using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleTaskDurationDays;
using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleTaskTitle;

namespace Engineering.Application.Services.ProjectWbses;

public partial class ProjectWbsLogic
{
    public async Task<Result<EditProjectScheduleTaskDurationDaysResponse>> EditProjectScheduleTaskDurationDaysCommand(
        EditProjectScheduleTaskDurationDaysRequest request, CT ct)
    {
        try
        {
            var entity = await _taskRepository.GetById(request.TaskId, ct);
            if (entity is null)
                return Result.Failure<EditProjectScheduleTaskDurationDaysResponse>(ProjectErrors.ProjectTaskNotFound)!;

            var hasChildren = await _taskRepository.HasChildren(entity.Id, ct);
            if (hasChildren)
                return Result.Failure<EditProjectScheduleTaskDurationDaysResponse>(ProjectErrors.SummaryTaskDurationNotEditable)!;

            long durationMinutes;
            if (request.Minutes.HasValue && request.Minutes.Value > 0)
            {
                durationMinutes = request.Minutes.Value;
            }
            else
            {
                var projectId = entity.ProjectScheduleImport!.ProjectId;
                var calendars = await _projectCalendarRepository.GetByProjectId(projectId, ct);
                var minutesPerDay = calendars?.FirstOrDefault(c => c.IsDefault)?.MinutesPerDay ?? 480;
                durationMinutes = (request.Days ?? 0) * minutesPerDay;
            }

            entity.SetPlannedDuration(durationMinutes, isEstimated: false);
            await _taskRepository.Update(entity);
            await _unitOfWork.CommitAsync(ct);
            return new EditProjectScheduleTaskDurationDaysResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EditProjectScheduleTaskDurationDaysResponse>(SharedErrors.UnknownError)!;
        }
    }
    public async Task<Result<EditProjectScheduleTaskTitleResponse>> EditProjectScheduleTaskTitleCommand(
        EditProjectScheduleTaskTitleRequest request, CT ct)
    {
        try
        {
            var title = request.Title?.Trim();
            if (string.IsNullOrEmpty(title) || title.Length > 250)
                return Result.Failure<EditProjectScheduleTaskTitleResponse>(ProjectErrors.ProjectTaskTitleInvalid)!;

            var entity = await _taskRepository.GetById(request.TaskId, ct);
            if (entity is null)
                return Result.Failure<EditProjectScheduleTaskTitleResponse>(ProjectErrors.ProjectTaskNotFound)!;

            entity.SetTitle(title);
            await _taskRepository.Update(entity);

            await _unitOfWork.CommitAsync(ct);
            return new EditProjectScheduleTaskTitleResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EditProjectScheduleTaskTitleResponse>(SharedErrors.UnknownError)!;
        }
    }
}
