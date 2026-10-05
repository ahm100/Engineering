namespace Engineering.Application.Services.MachineTypes.Models.MachineTypeModels;

public record GetsActiveMachineTypeModel
{
    public long Id { get; set; }
    public string MachineTypeCode { get; set; } = string.Empty;
    public string MachineTypeTitle { get; set; } = string.Empty;
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}