namespace Engineering.Application.Services.CostCenterTypes.Models.CostCenterTypeExcelImports;

public record CostCenterTypeExcelImportsRequest(
    IFormFile DocumentFile)
    : IHttpRequest;