namespace Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateForProject;

public record GetWbsTemplateForProjectResponse(
    List<GetWbsTemplateForProjectModel>? Data,
    int RowCount);

public record GetWbsTemplateForProjectModel
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}