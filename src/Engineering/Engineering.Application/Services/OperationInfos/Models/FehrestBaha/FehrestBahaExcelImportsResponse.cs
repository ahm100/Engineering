namespace Engineering.Application.Services.OperationInfos.Models.FehrestBaha;
public record FehrestBahaExcelImportError(
    string Field,
    string Value,
    string Message);

public record FehrestBahaExcelImportsResponse(
    bool IsSuccess,
    int OperationInfos,
    int OperationInfoSeasons,
    List<FehrestBahaExcelImportError> Errors);