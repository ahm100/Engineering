
namespace Engineering.Application.Services.Machineries.Models.MachineryExcelImports;

public record MachineryExcelImportsModel
{
    public string MachineryName { get; private set; } = string.Empty;
    public string MachineryCode { get; private set; } = string.Empty;
    public string GroupCode { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
}
