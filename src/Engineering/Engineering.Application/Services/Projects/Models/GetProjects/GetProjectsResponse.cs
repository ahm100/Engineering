using Engineering.Application.Services.Projects.Models.ProjectModels;

namespace Engineering.Application.Services.Projects.Models.GetProjects;

public record GetProjectsResponse(
    List<GetProjectsModel> Data,
    int RowCount
    );