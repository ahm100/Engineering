
namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.ProjectOperationTemporaryDailyGroupDelete;

public record ProjectOperationTemporaryDailyGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
