using Engineering.Application.Services.Projects.Models.ProjectModels;

namespace Engineering.Application.Services.Projects.Models.GetsByNameOrCode;

public record GetsByNameOrCodeResponse(
    List<GetProjectsModel> Data,
    int RowCount
    );
