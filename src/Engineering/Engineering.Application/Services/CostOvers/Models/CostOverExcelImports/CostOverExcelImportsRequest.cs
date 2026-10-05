namespace Engineering.Application.Services.CostOvers.Models.CostOverExcelImports;

public record CostOverExcelImportsRequest(
    IFormFile DocumentFile)
    : IHttpRequest;