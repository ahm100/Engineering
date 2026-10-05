namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistoryById;

public record GetDailyProjectOperationHistoryByIdRequest(
    long Id
    ) : IHttpRequest;
