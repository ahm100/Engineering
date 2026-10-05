using Engineering.Application.Services.CostCenters.Models.CostCenterModels;

namespace Engineering.Application.Services.CostCenters.Models.GetActiveCostCenters;

public record GetActiveCostCentersResponse(
    List<GetsActiveCostCentersModel> Data,
    int RowCount);
