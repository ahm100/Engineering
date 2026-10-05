
namespace Engineering.Application.Services.TransportationRequests.Models.SnapRequestExcelImports;

public record SnapRequestExcelImportsRequest(
    IFormFile DocumentFile
    ) : IHttpRequest;
