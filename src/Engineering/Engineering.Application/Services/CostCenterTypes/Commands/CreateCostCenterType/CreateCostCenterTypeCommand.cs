using CostCenterType = Engineering.Domain.Entities.CostCenters.CostCenterType;

namespace Engineering.Application.Services.CostCenterTypes.Commands.CreateCostCenterType;

public record CreateCostCenterTypeCommand(
    string CostCenterTypeName,
    string CostCenterTypeCode,
    bool IsActive,
    long? CompanyId)
    : ICommand<CostCenterType>;