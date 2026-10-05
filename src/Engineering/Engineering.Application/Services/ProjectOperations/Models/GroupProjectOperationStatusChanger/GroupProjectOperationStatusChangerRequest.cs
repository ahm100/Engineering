using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.GroupProjectOperationStatusChanger;

public record GroupProjectOperationStatusChangerRequest(
    List<long> Ids,
    ProjectOperationStatus Status
     ) : IHttpRequest;
