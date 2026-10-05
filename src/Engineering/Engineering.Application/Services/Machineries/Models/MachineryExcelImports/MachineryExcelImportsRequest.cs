
namespace Engineering.Application.Services.Machineries.Models.MachineryExcelImports;

public record MachineryExcelImportsRequest(
    IFormFile DocumentFile
    ) : IHttpRequest;
