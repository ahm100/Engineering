using CostCenterInformedUser = Engineering.Domain.Entities.CostCenters.CostCenterInformedUser;

namespace Engineering.Application.Services.CostCenterInformedUsers.Commands.DeleteInformedUser;

public record DeleteInformedUserCommand(
    long EmployeeId,
    long CostCenterId
    ) : ICommand<CostCenterInformedUser>;