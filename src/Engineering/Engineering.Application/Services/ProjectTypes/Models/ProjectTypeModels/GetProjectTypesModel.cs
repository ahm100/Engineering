namespace Engineering.Application.Services.ProjectTypes.Models.ProjectTypeModels;

public record GetsProjectTypeModel
{
    public long Id { get; set; }
    public string ProjectTypeCode { get; set; } = string.Empty;
    public string ProjectTypeTitle { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}


