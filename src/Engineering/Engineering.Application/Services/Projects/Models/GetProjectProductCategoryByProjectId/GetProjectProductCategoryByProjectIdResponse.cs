using Engineering.Application.Services.Projects.Models.GetProjectProductCategoryByCostCenterId;

namespace Engineering.Application.Services.Projects.Models.GetProjectProductCategoryByProjectId;

public record GetProjectProductCategoryByProjectIdResponse(
    List<GetProjectProductCategoryByCostCenterIdModel> Data,
    int RowCount);
