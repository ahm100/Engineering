namespace Engineering.Application.Services.ServiceInfos.Models.ServiceInfoExcelImports;

public record ServiceInfoExcelImportsRequest(
    IFormFile DocumentFile
    ) : IHttpRequest;
