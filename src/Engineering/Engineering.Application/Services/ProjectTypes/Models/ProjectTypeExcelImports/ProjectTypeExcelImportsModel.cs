namespace Engineering.Application.Services.ProjectTypes.Models.ProjectTypeExcelImports;

public record ProjectTypeExcelImportsModel
{
    public string ProjectTypeName { get; private set; } = string.Empty;
    public string ProjectTypeCode { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
}
