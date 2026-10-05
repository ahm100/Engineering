using Engineering.Application.Services.CostCenters.Models.CostCenterModels;

namespace Engineering.Application.Services.CostCenters.Models.GetContractorCostCenters;

public record GetContractorCostCentersResponse(
    List<GetContractorCostCentersModel> Data,
    int RowCount);
