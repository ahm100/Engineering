
namespace Engineering.Application.Services.MachineTypes.Models.MachineTypeExcelImports;

public record MachineTypeExcelImportsRequest(
    IFormFile DocumentFile
    ) : IHttpRequest;
