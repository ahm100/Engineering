using Engineering.Application.Services.CostCenterTypes.Models.CreateCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByCode;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeById;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByName;
using Engineering.Application.Services.CostCenterTypes.Models.GetsActiveCostCenterTypes;
using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterTypeExcelExporter;
using Engineering.Application.Services.CostCenterTypes.Models.UpdateCostCenterType;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Mappers.CostCenterTypes;

public class CostCenterTypesModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CostCenterType, CreateCostCenterTypeResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.CostCenterTypeCode, s => s.CostCenterTypeCode)
           .Map(d => d.CostCenterTypeName, s => s.CostCenterTypeTitle);

        config.NewConfig<CostCenterType, UpdateCostCenterTypeResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.CostCenterTypeCode, s => s.CostCenterTypeCode)
           .Map(d => d.CostCenterTypeName, s => s.CostCenterTypeTitle);
    }
}