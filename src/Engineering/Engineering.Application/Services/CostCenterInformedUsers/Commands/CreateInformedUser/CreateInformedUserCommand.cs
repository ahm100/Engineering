using Engineering.Domain.Entities.CostCenters;
using CostCenterInformedUser = Engineering.Domain.Entities.CostCenters.CostCenterInformedUser;

namespace Engineering.Application.Services.CostCenterInformedUsers.Commands.CreateInformedUser;

public record CreateInformedUserCommand(
    CostCenter CostCenter,
    long EmployeeId
    ) : ICommand<CostCenterInformedUser>;