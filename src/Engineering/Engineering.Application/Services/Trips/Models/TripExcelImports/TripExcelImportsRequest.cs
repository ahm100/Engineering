
namespace Engineering.Application.Services.Trips.Models.TripExcelImports;

public record TripExcelImportsRequest(
    IFormFile DocumentFile
    ) : IHttpRequest;
