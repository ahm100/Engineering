namespace Engineering.Application.Services.Categories.Models.CategoryExcelImports;

public record CategoryExcelImportsModel
{
    public string CategoryName { get; private set; } = string.Empty;
    public string CategoryCode { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
}