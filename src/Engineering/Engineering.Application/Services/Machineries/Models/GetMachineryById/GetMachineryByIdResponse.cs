using Engineering.Application.Services.Machineries.Models.MachineryModels;

namespace Engineering.Application.Services.Machineries.Models.GetMachineryById;

public record GetMachineryByIdResponse
{
    public long Id { get; set; }
    public string MachineryCode { get; set; } = string.Empty;
    public string MachineryName { get; set; } = string.Empty;
    public long GroupId { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public string GroupCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public MachineriesGroupModel? MachineriesGroup { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}
