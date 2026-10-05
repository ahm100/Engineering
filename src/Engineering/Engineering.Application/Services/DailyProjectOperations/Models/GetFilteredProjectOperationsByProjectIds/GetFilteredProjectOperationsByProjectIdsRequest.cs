namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredProjectOperationsByProjectIds;

public record GetFilteredProjectOperationsByProjectIdsRequest(
    List<long> ProjectIds,
    long CostCenterId,
    long? ContractorId,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
