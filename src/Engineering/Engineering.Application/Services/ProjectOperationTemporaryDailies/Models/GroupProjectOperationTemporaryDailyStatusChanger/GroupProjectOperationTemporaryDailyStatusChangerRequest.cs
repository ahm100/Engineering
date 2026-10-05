using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GroupProjectOperationTemporaryDailyStatusChanger;

public record GroupProjectOperationTemporaryDailyStatusChangerRequest(
    List<long> Ids,
    TemporaryDailyStatus Status
     ) : IHttpRequest;
