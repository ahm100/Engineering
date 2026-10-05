using Engineering.Domain.Entities.EngineeringDocs.Enums;

namespace Engineering.Application.Services.EngineeringDocs.Contracts.CreateProjectDocHistory;

public class GetProjectDocHistoriesModel
{
    public long Id { get; set; }
    public long ProjectDocId { get; set; }
    public long ProjectId { get; set; }
    public long DisciplineId { get; set; }
    public long DisciplineDocId { get; set; }
    public long ThirdPartyId { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int Revision { get; set; }
    public int Sequence { get; set; }
    public ProjectDocStatusEnum Status { get; set; }
    public long CreatorId { get; set; }
    public DateTime Created { get; set; }
}