namespace Engineering.Application.Services.MachineTypes.Models.GetMachineTypeById;

public record GetMachineTypeByIdResponse
{
    public long Id { get; set; }
    public string MachineTypeCode { get; set; } = string.Empty;
    public string MachineTypeTitle { get; set; } = string.Empty;
    public int FromWeight { get; set; }
    public int UntilWeight { get; set; }
    public long CabinTypeId { get; set; }
    public string CabinTypeName { get; set; } = string.Empty;
    public int CabinTypeCode { get; set; }
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}