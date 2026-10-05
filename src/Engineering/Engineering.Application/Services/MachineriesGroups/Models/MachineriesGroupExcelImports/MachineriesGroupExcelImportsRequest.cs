
namespace Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupExcelImports;

public record MachineriesGroupExcelImportsRequest(
    IFormFile DocumentFile
    ) : IHttpRequest;
