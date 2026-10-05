
namespace Engineering.Application.Services.CostCenters.Models.GetsActiveAuthorizedCostCenter;

public record GetsActiveAuthorizedCostCenterResponse(
    List<GetsActiveAuthorizedCostCenterModel> Data,
    int RowCount);
