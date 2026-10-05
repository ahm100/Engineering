namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationContractors;

public record GetDailyProjectOperationContractorsRequest(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
