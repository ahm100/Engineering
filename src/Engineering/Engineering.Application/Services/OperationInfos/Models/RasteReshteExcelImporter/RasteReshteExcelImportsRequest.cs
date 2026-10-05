namespace Engineering.Application.Services.OperationInfos.Models.RasteReshteExcelImporter;

public record RasteReshteExcelImportsRequest(
    long companyId,
    IFormFile DocumentFile);
