using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.CreateCostCenterVirtualGroupAdmin;

public record CreateCostCenterVirtualGroupAdminCommand(long ThirdPartyId,
                                                       string UserName,
                                                       CostCenterVirtualGroup CostCenterVirtualGroup) : ICommand<CostCenterVirtualGroupAdmin>;
