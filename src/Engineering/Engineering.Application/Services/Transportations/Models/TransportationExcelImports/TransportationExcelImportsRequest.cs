
namespace Engineering.Application.Services.Transportations.Models.TransportationExcelImports;

public record TransportationExcelImportsRequest(
    IFormFile DocumentFile
    ) : IHttpRequest;
