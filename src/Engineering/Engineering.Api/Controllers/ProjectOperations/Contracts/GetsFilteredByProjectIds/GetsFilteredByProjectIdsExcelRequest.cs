namespace Engineering.Api.Controllers.ProjectOperations.Contracts.GetsFilteredByProjectIds;

public record GetsFilteredByProjectIdsExcelRequest(
    List<long>? ProjectIds,
    List<long>? NotShowProjectOperationIds,
    List<FltrProjectOperationEnum>? ExcelFilters,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
