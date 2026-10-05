namespace Engineering.Application.Services.ProjectWbses.Contracts.SetProjectScheduleTaskValue;

public record SetProjectScheduleTaskValueRequest(
    long TaskId,
    long ColumnId,
    string? Value,
    decimal? NumberValue,
    DateTime? DateTimeValue) : IHttpRequest;