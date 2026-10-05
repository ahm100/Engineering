namespace Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDoc;

public record GetDisciplineDocResponse(
    List<GetDisciplineDocModel> Data,
    int RowCount);

public record GetDisciplineDocModel : IUserAuditable
{
    public long Id { get; set; }

    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public long CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }

    public long? UpdaterId { get; set; }
    public string? Updater { get; set; } = string.Empty;
    public DateTime? Updated { get; set; }
}
