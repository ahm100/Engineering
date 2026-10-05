namespace Engineering.Application.Services.OperationInfoGroups.Models.OperationInfoGroupExcelImports;

public record OperationInfoGroupExcelImportsModel
{
    public string OperationInfoGroupName { get; private set; } = string.Empty;
    public string OperationInfoGroupCode { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
}
