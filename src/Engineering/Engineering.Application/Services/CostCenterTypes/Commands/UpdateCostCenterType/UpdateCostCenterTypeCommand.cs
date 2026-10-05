using CostCenterType = Engineering.Domain.Entities.CostCenters.CostCenterType;

namespace Engineering.Application.Services.CostCenterTypes.Commands.UpdateCostCenterType;

public record UpdateCostCenterTypeCommand(
    long Id,
    string CostCenterTypeName,
    string CostCenterTypeCode,
    bool IsActive,
    long? CompanyId)
    : ICommand<CostCenterType>;