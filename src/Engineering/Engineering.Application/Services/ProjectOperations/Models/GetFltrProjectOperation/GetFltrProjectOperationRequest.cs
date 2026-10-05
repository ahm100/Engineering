namespace Engineering.Application.Services.ProjectOperations.Models.GetFltrProjectOperation;

public record GetFltrProjectOperationRequest(
    long CostCenterId,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    List<long>? MeasureUnitIds,
    decimal? MinPrice,
    decimal? MaxPrice,
    string? FilterData,
    int PageIndex,
    int PageSize
) : IHttpRequest;
