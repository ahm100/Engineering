
namespace Engineering.Application.Services.DailyProjectOperations.Models.DeleteDailyProjectOperation;

public record DeleteDailyProjectOperationRequest(
    long Id
    ) : IHttpRequest;
