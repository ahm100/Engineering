
using Engineering.Application.Services.Projects.Models.ProjectModels;

namespace Engineering.Application.Services.Projects.Models.GetsProjectSorting;

public record GetsProjectSortingResponse(
    List<GetProjectsModel> Data,
    int RowCount
    );
