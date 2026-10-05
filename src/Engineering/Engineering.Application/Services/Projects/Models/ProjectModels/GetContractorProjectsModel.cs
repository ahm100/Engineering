namespace Engineering.Application.Services.Projects.Models.ProjectModels;

public record GetContractorProjectsModel
{
    public long Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public bool Contractual { get; set; }
}


