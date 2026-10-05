namespace Engineering.Application.Services.CabinTypes.Models.CabinTypeExcelImports;

public record CabinTypeExcelImportsRequest(
    IFormFile DocumentFile)
    : IHttpRequest;