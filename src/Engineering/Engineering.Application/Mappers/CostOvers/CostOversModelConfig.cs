using Engineering.Application.Services.CostOvers.Models.GetCostOverByCode;
using Engineering.Application.Services.CostOvers.Models.GetCostOverById;
using Engineering.Application.Services.CostOvers.Models.GetCostOverByName;
using Engineering.Application.Services.CostOvers.Models.GetsActiveCostOvers;
using Engineering.Application.Services.CostOvers.Models.GetsCostOverExcelExporter;
using Engineering.Application.Services.CostOvers.Models.GetsCostOvers;
using Engineering.Domain.Entities.CostOvers;

namespace Engineering.Application.Mappers.CostOvers;

public class CostOversModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CostOver, GetsCostOverExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.CostOverName, s => s.CostOverName)
           .Map(d => d.CostOverCode, s => s.CostOverCode)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.IsActive, s => s.IsActive);
    }
}
