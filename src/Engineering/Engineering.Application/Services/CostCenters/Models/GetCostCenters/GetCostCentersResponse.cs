using Engineering.Application.Services.CostCenters.Models.CostCenterModels;

namespace Engineering.Application.Services.CostCenters.Models.GetCostCenters;

public record GetCostCentersResponse(
    List<GetCostCentersModel> Data,
    int RowCount);
