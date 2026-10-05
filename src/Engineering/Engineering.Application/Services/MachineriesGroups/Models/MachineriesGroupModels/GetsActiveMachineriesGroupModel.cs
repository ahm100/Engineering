namespace Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupModels;

public record GetsActiveMachineriesGroupModel
{
    public long Id { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public string GroupCode { get; set; } = string.Empty;
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}
