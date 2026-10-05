using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.UpdateCostCenterVirtualGroupAdmin;

public record UpdateCostCenterVirtualGroupAdminCommand(long CostCenterVirtualGroupAdminId,
                                                       long ThirdPartyId,
                                                       string UserName) : ICommand<CostCenterVirtualGroupAdmin>;
