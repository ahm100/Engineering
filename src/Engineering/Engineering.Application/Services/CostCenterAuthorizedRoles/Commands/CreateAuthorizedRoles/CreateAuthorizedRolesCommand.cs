using Engineering.Domain.Entities.CostCenters;
using CostCenterAuthorizedRole = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedRole;

namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Commands.CreateAuthorizedRoles;

public record CreateAuthorizedRolesCommand(
    CostCenter CostCenter,
    List<long> RoleIds
    ) : ICommand<List<CostCenterAuthorizedRole?>>;