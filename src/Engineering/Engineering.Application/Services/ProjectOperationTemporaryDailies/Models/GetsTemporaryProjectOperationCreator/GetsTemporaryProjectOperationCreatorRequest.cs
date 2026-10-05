using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetCurrentUserTemporaryDailies;

public record GetCurrentUserTemporaryDailiesRequest(
    long? CostCenterId,
    long? ProjectId,
    long? ProjectOperationId,
    TemporaryDailyStatus? Status,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize) : IHttpRequest;
