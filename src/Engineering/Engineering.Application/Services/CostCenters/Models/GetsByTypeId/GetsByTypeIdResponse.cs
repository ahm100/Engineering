using Engineering.Application.Services.CostCenters.Models.CostCenterModels;

namespace Engineering.Application.Services.CostCenters.Models.GetsByTypeId;

public record GetsByTypeIdResponse(
    List<GetsByTypeIdModel> Data,
    int RowCount);
