namespace Engineering.Api.Controllers.WbsTemplates.Contracts.GetFltrWbsTemplate;

public record GetFltrWbsTemplateToExcelRequest(
    string? FilterData,
    List<WbsTemplateEnum>? ExcelFilters,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;