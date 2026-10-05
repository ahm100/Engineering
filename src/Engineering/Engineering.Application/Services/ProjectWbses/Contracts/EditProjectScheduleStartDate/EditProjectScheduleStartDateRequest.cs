namespace Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleStartDate;

public record EditProjectScheduleStartDateRequest(
    long ProjectId,
    DateTime StartDate) : IHttpRequest;