
namespace Engineering.Application.Services.MachineTypes.Models.GetsMachineTypeExcelExporter;

public record GetsMachineTypeExcelExporterModel
{
    public long Id { get; set; }
    public string MachineTypeName { get; set; } = string.Empty;
    public string MachineTypeCode { get; set; } = string.Empty;
    public int FromWeight { get; set; }
    public int UntilWeight { get; set; }
    public long CabinTypeId { get; set; }
    public string CabinTypeName { get; set; } = string.Empty;
    public int CabinTypeCode { get; set; }
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
};
