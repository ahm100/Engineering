namespace Engineering.Application.Services.Adjustments.Contracts;

public record AdjustmentExcelImportsRequest(
    long CompanyId,
    long YearId,
    string? NotificationFileName,
    IFormFile DocumentFile);