namespace Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectScheduleColumn;

public record RemoveProjectScheduleColumnRequest(
    long Id) : IHttpRequest;