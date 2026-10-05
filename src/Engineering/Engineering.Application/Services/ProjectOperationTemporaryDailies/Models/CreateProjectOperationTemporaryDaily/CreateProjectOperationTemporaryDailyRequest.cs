using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.CreateProjectOperationTemporaryDaily;

public record CreateProjectOperationTemporaryDailyRequest(
    long CostCenterId,
    long projectId,
    long? ProjectOperationId,
    TemporaryDailyStatus? Status,
    DateTime StartDate,
    DateTime EndDate,
    string? Description,
    List<string>? Documents) : IHttpRequest;
