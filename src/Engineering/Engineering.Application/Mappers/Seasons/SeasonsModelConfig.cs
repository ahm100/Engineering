using Engineering.Application.Services.Seasons.Models.GetsSeasonExcelExporter;
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.Mappers.Seasons;

public class SeasonsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Season, GetsSeasonExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.SeasonName, s => s.SeasonName)
           .Map(d => d.SeasonCode, s => s.SeasonCode)
           .Map(d => d.BranchId, s => s.Branch.Id)
           .Map(d => d.BranchName, s => s.Branch.BranchName)
           .Map(d => d.BranchCode, s => s.Branch.BranchCode)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId);
    }
}