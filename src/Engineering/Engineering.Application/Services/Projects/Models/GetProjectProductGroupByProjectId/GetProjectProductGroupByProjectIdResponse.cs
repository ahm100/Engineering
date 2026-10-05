using Engineering.Application.Services.Projects.Models.GetProjectProductGroupByCostCenterId;

namespace Engineering.Application.Services.Projects.Models.GetProjectProductGroupByProjectId;

public record GetProjectProductGroupByProjectIdResponse(
    List<GetProjectProductModel> Data,
    int RowCount);
