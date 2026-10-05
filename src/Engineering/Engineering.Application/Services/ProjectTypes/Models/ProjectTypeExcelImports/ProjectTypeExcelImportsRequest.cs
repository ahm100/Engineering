
namespace Engineering.Application.Services.ProjectTypes.Models.ProjectTypeExcelImports;

public record ProjectTypeExcelImportsRequest(
    IFormFile DocumentFile
    ) : IHttpRequest;
