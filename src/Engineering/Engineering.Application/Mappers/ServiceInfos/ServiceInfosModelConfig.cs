using Engineering.Application.Services.ServiceInfos.Models.GetServiceByCode;
using Engineering.Application.Services.ServiceInfos.Models.GetServiceById;
using Engineering.Application.Services.ServiceInfos.Models.GetServiceByName;
using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoExcelExporter;
using Engineering.Domain.Entities.ServiceInfos;

namespace Engineering.Application.Mappers.ServiceInfos;

public class ServiceInfosModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ServiceInfo, GetsServiceInfoExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ServiceInfoName, s => s.ServiceInfoName)
           .Map(d => d.ServiceInfoCode, s => s.ServiceInfoCode)
           .Map(d => d.MeasurementId, s => s.UnitOfMeasurementId)
           .Map(d => d.IsActive, s => s.IsActive)
           ;
        config.NewConfig<ServiceInfo, GetServiceInfoByIdResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ServiceInfoName, s => s.ServiceInfoName)
           .Map(d => d.ServiceInfoCode, s => s.ServiceInfoCode)
           .Map(d => d.MeasurementData.Id, s => s.UnitOfMeasurementId)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<ServiceInfo, GetServiceInfoByNameResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ServiceInfoName, s => s.ServiceInfoName)
           .Map(d => d.ServiceInfoCode, s => s.ServiceInfoCode)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<ServiceInfo, GetServiceInfoByCodeResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ServiceInfoName, s => s.ServiceInfoName)
           .Map(d => d.ServiceInfoCode, s => s.ServiceInfoCode)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
    }
}