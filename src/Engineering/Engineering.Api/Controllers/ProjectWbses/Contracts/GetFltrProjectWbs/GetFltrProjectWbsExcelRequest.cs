namespace Engineering.Api.Controllers.ProjectWbses.Contracts.GetFltrProjectWbs;

public record GetFltrProjectWbsExcelRequest(
    List<long>? ProjectIds,
    List<long>? WbsTemplateIds,
    string? FilterData,
    List<FltrProjectWbsEnum> ExcelFilters,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;