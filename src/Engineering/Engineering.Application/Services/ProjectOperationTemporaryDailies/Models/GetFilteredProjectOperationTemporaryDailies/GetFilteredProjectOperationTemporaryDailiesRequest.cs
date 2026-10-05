using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetFilteredProjectOperationTemporaryDailies;

public record GetFilteredProjectOperationTemporaryDailiesRequest(
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
