using Engineering.Application.Services.MachineTypes.Models.GetMachineTypeById;
using Engineering.Application.Services.MachineTypes.Models.GetsMachineTypeExcelExporter;
using Engineering.Application.Services.MachineTypes.Models.MachineTypeModels;
using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Mappers.MachineTypes;

public class MachineTypesModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MachineType, GetsMachineTypeExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.MachineTypeName, s => s.MachineTypeTitle)
           .Map(d => d.MachineTypeCode, s => s.MachineTypeCode)
           .Map(d => d.FromWeight, s => s.FromWeight)
           .Map(d => d.UntilWeight, s => s.UntilWeight)
           .Map(d => d.CabinTypeId, s => s.CabinType.Id)
           .Map(d => d.CabinTypeCode, s => s.CabinType.CabinTypeCode)
           .Map(d => d.CabinTypeName, s => s.CabinType.CabinTypeName)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
    }
}