namespace Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupByName;

public record GetMachineriesGroupByNameResponse
{
    public long Id { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public string GroupCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}