namespace Engineering.Application.Services.OperationInfos.Models.RasteReshteExcelImporter;
public record RasteReshteExcelImportsResponse(
    bool IsSuccess,
    int Categories,
    int Branches,
    int Seasons);