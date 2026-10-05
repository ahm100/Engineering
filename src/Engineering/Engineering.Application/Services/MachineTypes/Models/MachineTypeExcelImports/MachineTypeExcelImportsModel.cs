namespace Engineering.Application.Services.MachineTypes.Models.MachineTypeExcelImports;

public record MachineTypeExcelImportsModel
{
    public string MachineTypeName { get; private set; } = string.Empty;
    public string MachineTypeCode { get; private set; } = string.Empty;
    public int FromWeight { get; private set; }
    public int UntilWeight { get; private set; }
    public int CabinTypeCode { get; private set; }
    public bool IsActive { get; private set; } = true;
}
