namespace Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupExcelImports;

public record MachineriesGroupExcelImportsModel
{
    public string MachineriesGroupName { get; private set; } = string.Empty;
    public string MachineriesGroupCode { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
}
