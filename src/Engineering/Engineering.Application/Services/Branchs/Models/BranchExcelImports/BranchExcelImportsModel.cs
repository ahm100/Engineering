namespace Engineering.Application.Services.Branchs.Models.BranchExcelImports;

public record BranchExcelImportsModel
{
    public string BranchName { get; private set; } = string.Empty;
    public string BranchCode { get; private set; } = string.Empty;
    public string CategoryCode { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
}