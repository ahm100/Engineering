
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Queries.GetProjectModelById;

public record GetProjectModelByIdQuery(
    long Id
    ) : IQuery<GetProjectModelByIdReponse>;

public record GetProjectModelByIdReponse
{
    public long Id { get; set; }
    public ProjectStatus Status { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public Guid PreferentialReferenceCode { get; set; }
    public string? ProjectCode { get; set; } = string.Empty;
    public string? Prefix { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? CostCenterCode { get; set; } = string.Empty;
    public long? ProjectManagerId { get; set; }
    public string? ProjectManagerName { get; set; } = string.Empty;
}
