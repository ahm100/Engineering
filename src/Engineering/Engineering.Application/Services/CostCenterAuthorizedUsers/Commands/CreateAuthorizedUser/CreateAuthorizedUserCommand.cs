using Engineering.Domain.Entities.CostCenters;
using CostCenterAuthorizedUser = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedUser;

namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Commands.CreateAuthorizedUser;

public record CreateAuthorizedUserCommand(
    CostCenter CostCenter,
    long AuthorizedUserId
    ) : ICommand<CostCenterAuthorizedUser>;