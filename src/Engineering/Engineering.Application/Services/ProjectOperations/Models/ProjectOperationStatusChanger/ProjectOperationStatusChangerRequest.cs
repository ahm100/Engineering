using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.ProjectOperationStatusChanger;

public record ProjectOperationStatusChangerRequest(
    long Id,
    ProjectOperationStatus Status
     ) : IHttpRequest;
