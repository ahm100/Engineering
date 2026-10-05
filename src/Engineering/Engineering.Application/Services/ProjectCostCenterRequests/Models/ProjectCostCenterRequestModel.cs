using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectCostCenterRequests.Models;

public record ProjectCostCenterRequestModel : IUserAuditable
{
    public long Id { get; set; }
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public long? CityId { get; set; }
    public string? CityName { get; set; }
    public string? RequestedCostCenterName { get; set; }
    public string Description { get; set; } = string.Empty;
    public ProjectCostCenterRequestStatus Status { get; set; }
    public string StatusTitle => Status.GetEnumDescription();
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public string? RejectionReason { get; set; }

    public long CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public long? UpdaterId { get; set; }
    public string? Updater { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
    public DateTime? Updated { get; set; }
    public string? UpdatedShamsi => Updated.ToShamsi();
}