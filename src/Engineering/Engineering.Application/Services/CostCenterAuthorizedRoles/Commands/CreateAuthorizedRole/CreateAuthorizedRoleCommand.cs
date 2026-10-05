using Engineering.Domain.Entities.CostCenters;
using CostCenterAuthorizedRole = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedRole;

namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Commands.CreateAuthorizedRole;

public record CreateAuthorizedRoleCommand(
    CostCenter CostCenter,
    long AuthorizedRoleId
    ) : ICommand<CostCenterAuthorizedRole>;