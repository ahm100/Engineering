namespace Engineering.Application.Services.Categories.Models.CategoryExcelImports;

public record CategoryExcelImportsRequest(
    IFormFile DocumentFile)
    : IHttpRequest;