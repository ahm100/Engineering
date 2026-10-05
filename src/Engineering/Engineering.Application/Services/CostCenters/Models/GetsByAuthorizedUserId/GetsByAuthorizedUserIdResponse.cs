using Engineering.Application.Services.CostCenters.Models.CostCenterModels;

namespace Engineering.Application.Services.CostCenters.Models.GetsByAuthorizedUserId;

public record GetsByAuthorizedUserIdResponse(
    List<CostCentersByAuthorizedUserIdModel> Data,
    int RowCount);
