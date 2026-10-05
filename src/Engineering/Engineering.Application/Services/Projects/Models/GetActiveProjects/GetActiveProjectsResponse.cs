using Engineering.Application.Services.Projects.Models.ProjectModels;

namespace Engineering.Application.Services.Projects.Models.GetActiveProjects;

public record GetActiveProjectsResponse(
    List<GetsActiveProjectModel> Data,
    int RowCount);

