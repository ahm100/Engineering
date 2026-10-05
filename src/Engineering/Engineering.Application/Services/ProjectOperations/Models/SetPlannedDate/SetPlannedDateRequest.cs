namespace Engineering.Application.Services.ProjectOperations.Models.SetPlannedDate;

public record SetPlannedDateRequest(
    long Id,
    DateTime? PlannedStartDate,
    DateTime? PlannedFinishDate,
    int? PlannedDuration) : IHttpRequest;