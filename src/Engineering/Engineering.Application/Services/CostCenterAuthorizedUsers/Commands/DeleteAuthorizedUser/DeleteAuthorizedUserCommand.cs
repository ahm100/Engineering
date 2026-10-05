using CostCenterAuthorizedUser = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedUser;

namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Commands.DeleteAuthorizedUser;

public record DeleteAuthorizedUserCommand(
    long AuthorizedUserId,
    long CostCenterId
    ) : ICommand<CostCenterAuthorizedUser>;