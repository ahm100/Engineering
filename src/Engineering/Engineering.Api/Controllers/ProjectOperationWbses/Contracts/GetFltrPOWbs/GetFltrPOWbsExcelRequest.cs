namespace Engineering.Api.Controllers.ProjectOperationWbses.Contracts.GetFltrPOWbs;

public record GetFltrPOWbsExcelRequest(
    long? ProjectId,
    List<long>? ProjectOperationIds,
    List<long>? ProjectWbsIds,
    string? FilterData,
    List<FltrPOWbsEnum>? ExcelFilters,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;