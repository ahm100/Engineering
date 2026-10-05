using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.ProjectOperationTemporaryDailyStatusChanger;

public record ProjectOperationTemporaryDailyStatusChangerRequest(
    long Id,
    TemporaryDailyStatus Status
     ) : IHttpRequest;
