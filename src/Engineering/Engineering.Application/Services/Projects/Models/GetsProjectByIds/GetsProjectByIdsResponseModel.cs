
namespace Engineering.Application.Services.Projects.Models.GetsProjectByIds;

public record GetsProjectByIdsResponseModel
{
    public long Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public bool HasProduct { get; set; }
    public bool Contractual { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}
