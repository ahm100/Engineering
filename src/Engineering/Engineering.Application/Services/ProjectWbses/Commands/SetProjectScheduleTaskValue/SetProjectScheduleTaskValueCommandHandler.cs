using System.Globalization;
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.Contracts.SetProjectScheduleTaskValue;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.SetProjectScheduleTaskValue;

public class SetProjectScheduleTaskValueCommandHandler: ICommandHandler<SetProjectScheduleTaskValueCommand, SetProjectScheduleTaskValueResponse>
{
    private readonly ILogger<SetProjectScheduleTaskValueCommandHandler> _logger;
    private readonly IProjectScheduleTaskRepository _taskRepository;
    private readonly IProjectScheduleColumnRepository _columnRepository;
    private readonly IProjectScheduleTaskValueRepository _valueRepository;

    public SetProjectScheduleTaskValueCommandHandler(
        ILogger<SetProjectScheduleTaskValueCommandHandler> logger,
        IProjectScheduleTaskRepository taskRepository,
        IProjectScheduleColumnRepository columnRepository,
        IProjectScheduleTaskValueRepository valueRepository)
    {
        _logger = logger;
        _taskRepository = taskRepository;
        _columnRepository = columnRepository;
        _valueRepository = valueRepository;
    }

    public async Task<Result<SetProjectScheduleTaskValueResponse>> Handle(
        SetProjectScheduleTaskValueCommand command, CT ct)
    {
        try
        {
            var task = await _taskRepository.GetById(command.TaskId, ct);
            if (task is null)
                return Result.Failure<SetProjectScheduleTaskValueResponse>(ProjectErrors.ProjectTaskNotFound)!;

            var column = await _columnRepository.GetById(command.ColumnId, ct);
            if (column is null)
                return Result.Failure<SetProjectScheduleTaskValueResponse>(ProjectErrors.ColumnNotFound)!;

            if (column.ColumnType != ProjectScheduleColumnType.Custom)
                return Result.Failure<SetProjectScheduleTaskValueResponse>(ProjectErrors.ColumnIsNotCustom)!;

            if (task.ProjectScheduleImportId != column.ProjectScheduleImportId)
                return Result.Failure<SetProjectScheduleTaskValueResponse>(
                    ProjectErrors.ColumnAndTaskScheduleMismatch)!;

            var text = string.IsNullOrWhiteSpace(command.Value) ? null : command.Value.Trim();
            var number = command.NumberValue;
            var date = command.DateTimeValue;

            var wrongField = column.DataType switch
            {
                ProjectScheduleColumnDataType.Decimal => text is not null || date is not null,
                ProjectScheduleColumnDataType.DateTime => text is not null || number is not null,
                _ => number is not null || date is not null
            };
            if (wrongField)
                return Result.Failure<SetProjectScheduleTaskValueResponse>(ProjectErrors.ColumnValueInvalid)!;

            if (text is not null && text.Length > 1500)
                return Result.Failure<SetProjectScheduleTaskValueResponse>(ProjectErrors.ColumnValueInvalid)!;

            var isEmpty = text is null && number is null && date is null;

            var value = await _valueRepository.GetByTaskAndColumnId(task.Id, column.Id, ct);

            if (value is null && isEmpty)
                return new SetProjectScheduleTaskValueResponse(true);

            var isNew = value is null;
            value ??= new ProjectScheduleTaskValue(task, column);

            switch (column.DataType)
            {
                case ProjectScheduleColumnDataType.Decimal: value.SetNumber(number); break;
                case ProjectScheduleColumnDataType.DateTime: value.SetDateTime(date); break;
                default: value.SetText(text); break;
            }

            if (isNew)
                await _valueRepository.Create(value, ct);

            return new SetProjectScheduleTaskValueResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<SetProjectScheduleTaskValueResponse>(SharedErrors.UnknownError)!;
        }
    }
}