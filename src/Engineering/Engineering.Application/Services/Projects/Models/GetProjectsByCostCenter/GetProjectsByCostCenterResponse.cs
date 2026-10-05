using Engineering.Application.Services.Projects.Models.ProjectModels;

namespace Engineering.Application.Services.Projects.Models.GetProjectsByCostCenter;

public record GetProjectsByCostCenterResponse(
    List<GetProjectsByCostCenterModel> Data,
    int RowCount
    );
