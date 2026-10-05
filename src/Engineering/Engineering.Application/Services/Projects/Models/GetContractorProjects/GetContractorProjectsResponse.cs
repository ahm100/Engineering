using Engineering.Application.Services.Projects.Models.ProjectModels;

namespace Engineering.Application.Services.Projects.Models.GetContractorProjects;

public record GetContractorProjectsResponse(
    List<GetContractorProjectsModel> Data,
    int RowCount);
