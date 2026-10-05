using Engineering.Application.Services.Projects.Models.ProjectModels;

namespace Engineering.Application.Services.Projects.Models.GetsActiveProjectByCostCenterIds;

public record GetsActiveProjectByCostCenterIdsResponse(
    List<GetsActiveProjectModel> Data,
    int RowCount);

