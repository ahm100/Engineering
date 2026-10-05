using CostCenterAuthorizedRole = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedRole;

namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Commands.DeleteAuthorizedRole;

public record DeleteAuthorizedRoleCommand(
    long AuthorizedRoleId,
    long CostCenterId
    ) : ICommand<CostCenterAuthorizedRole>;