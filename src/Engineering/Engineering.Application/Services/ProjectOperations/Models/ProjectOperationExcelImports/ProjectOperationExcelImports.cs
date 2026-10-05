using System.ComponentModel;

namespace Engineering.Application.Services.ProjectOperations.Models.ProjectOperationExcelImports;

public class ProjectOperationExcelImportsRequest : IHttpRequest
{
    public long ProjectId { get; set; }
    public required IFormFile DocumentFile { get; set; }
}

// Return the error models so the Api layer can build the file
public record ProjectOperationExcelImportsResponse(
    bool IsDone,
    List<ProjectOperationImportErrorModel>? ErrorModels = null);

public class ProjectOperationExcelImportsModel
{
    public string OperationInfoCode { get; set; } = string.Empty;
    public decimal TolerancePercentage { get; set; }
    public decimal Workload { get; set; }
    public string? StartDate { get; set; }
    public string? FinishDate { get; set; }
}

public class ProjectOperationImportErrorModel
{
    public string OperationInfoCode { get; set; } = string.Empty;
    public decimal TolerancePercentage { get; set; }
    public decimal Workload { get; set; }
    public string? StartDate { get; set; }
    public string? FinishDate { get; set; }
    public string Error { get; set; } = string.Empty;
}

public enum ProjectOperationImportErrorEnum
{
    [Description("کد شرح عملیات")]
    OperationInfoCode,

    [Description("درصد تلورانس")]
    TolerancePercentage,

    [Description("حجم کار")]
    Workload,

    [Description("تاریخ شروع")]
    StartDate,

    [Description("تاریخ پایان")]
    FinishDate,

    [Description("خطا / Error")]
    Error
}