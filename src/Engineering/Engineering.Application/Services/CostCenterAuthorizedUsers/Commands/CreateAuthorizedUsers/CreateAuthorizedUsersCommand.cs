using Engineering.Domain.Entities.CostCenters;
using CostCenterAuthorizedUser = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedUser;

namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Commands.CreateAuthorizedUsers;

public record CreateAuthorizedUsersCommand(
    CostCenter CostCenter,
    List<long> UserIds
    ) : ICommand<List<CostCenterAuthorizedUser?>>;