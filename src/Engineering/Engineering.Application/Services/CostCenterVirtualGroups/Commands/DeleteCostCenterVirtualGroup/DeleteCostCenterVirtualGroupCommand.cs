using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.DeleteCostCenterVirtualGroup;

public record DeleteCostCenterVirtualGroupCommand(long CostCenterVirtualGroupId) : ICommand<CostCenterVirtualGroup>;
