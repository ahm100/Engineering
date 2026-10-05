
namespace Engineering.Application.Services.Adjustments.Contracts;

public record AdjustmentImportError(
    string Field,
    string Value,
    string Message);

public record AdjustmentExcelImportsResponse(
    bool IsSuccess,
    int SeasonAdjustments,
    int BranchAdjustments,
    List<AdjustmentImportError> Errors);