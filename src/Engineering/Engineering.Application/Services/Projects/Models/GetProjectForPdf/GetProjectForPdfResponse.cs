using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetProjectForPdf;

public record GetProjectForPdfResponse()
{
    public long Id { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectCode { get; set; }
    public ProjectStatus? Status { get; set; }
    public string? StatusTitle => Status?.GetEnumDescription();
    public decimal? ApprovedBudget { get; set; }
    public long? EmployerId { get; set; }
    public string? EmployerName { get; set; }
    public long? AdvisorId { get; set; }
    public string? AdvisorName { get; set; }
    public long? ProjectManagerId { get; set; }
    public string? ProjectManagerName { get; set; }
}
