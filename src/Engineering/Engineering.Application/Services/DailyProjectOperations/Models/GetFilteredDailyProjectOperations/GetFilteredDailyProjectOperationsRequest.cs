namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredDailyProjectOperations;

public record GetFilteredDailyProjectOperationsRequest(
    long CostCenterId,
    long ProjectId,
    long? ContractorId,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
