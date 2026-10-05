namespace Engineering.Application.Services.Projects.Models.GetUnAssignedProjects;

public record GetUnAssignedProjectsResponse(
    List<GetUnAssignedProjectsModel> Data,
    int RowCount
    );

public record GetUnAssignedProjectsModel()
{
    public long Id { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectCode { get; set; }
    public long? CityId { get; set; }
    public string? City { get; set; }
}