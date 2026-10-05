
namespace Engineering.Application.Services.OperationInfoGroups.Models.OperationInfoGroupExcelImports;

public record OperationInfoGroupExcelImportsRequest(
    IFormFile DocumentFile
    ) : IHttpRequest;
