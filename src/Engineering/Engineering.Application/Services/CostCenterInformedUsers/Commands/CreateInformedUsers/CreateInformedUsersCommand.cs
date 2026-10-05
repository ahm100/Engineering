using Engineering.Domain.Entities.CostCenters;
using CostCenterInformedUser = Engineering.Domain.Entities.CostCenters.CostCenterInformedUser;

namespace Engineering.Application.Services.CostCenterInformedUsers.Commands.CreateInformedUsers;

public record CreateInformedUsersCommand(
    CostCenter CostCenter,
    List<long?>? EmployeeIds
    ) : ICommand<List<CostCenterInformedUser?>>;