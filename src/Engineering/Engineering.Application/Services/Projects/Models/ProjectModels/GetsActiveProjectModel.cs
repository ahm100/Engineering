namespace Engineering.Application.Services.Projects.Models.ProjectModels;

public record GetsActiveProjectModel
{
    public long Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public bool HasProduct { get; set; }
    public bool Contractual { get; set; }
    public string ContractualText => Contractual == true ? "قرارداد ناپذیر" : "قرارداد پذیر";
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}
