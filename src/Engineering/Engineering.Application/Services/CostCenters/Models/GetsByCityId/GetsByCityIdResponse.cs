using Engineering.Application.Services.CostCenters.Models.CostCenterModels;

namespace Engineering.Application.Services.CostCenters.Models.GetsByCityId;

public record GetsByCityIdResponse(
    List<GetsByCityIdModel> Data,
    int RowCount);
