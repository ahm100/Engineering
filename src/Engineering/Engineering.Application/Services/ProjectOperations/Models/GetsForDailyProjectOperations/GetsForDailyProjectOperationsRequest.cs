namespace Engineering.Application.Services.ProjectOperations.Models.GetsForDailyProjectOperations;

public record GetsForDailyProjectOperationsRequest(
    long ProjectId,
    int? Priority,
    string? FilterData,
    DateTime? StartDate,
    DateTime? EndDate,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
