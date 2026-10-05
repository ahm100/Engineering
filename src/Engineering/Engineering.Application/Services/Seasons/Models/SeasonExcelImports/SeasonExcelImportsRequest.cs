
namespace Engineering.Application.Services.Seasons.Models.SeasonExcelImports;

public record SeasonExcelImportsRequest(
    IFormFile DocumentFile
    ) : IHttpRequest;
