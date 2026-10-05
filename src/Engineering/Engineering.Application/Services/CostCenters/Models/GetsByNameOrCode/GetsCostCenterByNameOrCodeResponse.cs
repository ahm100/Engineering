using Engineering.Application.Services.CostCenters.Models.CostCenterModels;

namespace Engineering.Application.Services.CostCenters.Models.GetsByNameOrCode;

public record GetsCostCenterByNameOrCodeResponse(
    List<GetCostCentersModel> Data,
    int RowCount);
