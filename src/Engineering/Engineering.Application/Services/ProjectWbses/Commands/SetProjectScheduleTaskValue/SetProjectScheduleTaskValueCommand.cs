using Engineering.Application.Services.ProjectWbses.Contracts.SetProjectScheduleTaskValue;

namespace Engineering.Application.Services.ProjectWbses.Commands.SetProjectScheduleTaskValue;

public record SetProjectScheduleTaskValueCommand(
    long TaskId,
    long ColumnId,
    string? Value,
    decimal? NumberValue,
    DateTime? DateTimeValue) : ICommand<SetProjectScheduleTaskValueResponse>;