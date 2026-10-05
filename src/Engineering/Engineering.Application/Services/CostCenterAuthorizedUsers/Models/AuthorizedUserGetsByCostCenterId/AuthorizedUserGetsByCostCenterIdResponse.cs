using Engineering.Application.Services.CostCenterAuthorizedUsers.Models.AuthorizedUserModels;

namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Models.AuthorizedUserGetsByCostCenterId;

public record AuthorizedUserGetsByCostCenterIdResponse(
    List<AuthorizedUserGetsByCostCenterIdModel> Data,
    int RowCount);
