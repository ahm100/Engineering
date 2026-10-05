using Engineering.Application.Services.Transportations.Models.GetByCode;
using Engineering.Application.Services.Transportations.Models.GetById;
using Engineering.Application.Services.Transportations.Models.GetByName;
using Engineering.Application.Services.Transportations.Models.GetsActive;
using Engineering.Application.Services.Transportations.Models.GetsFiltered;
using Engineering.Application.Services.Transportations.Models.GetsTransportationExcelExporter;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Mappers.Transportations;

public class TransportationsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Transportation, GetsTransportationExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.TransportationName, s => s.TransportationName)
           .Map(d => d.TransportationCode, s => s.TransportationCode)
           .Map(d => d.IsPassenger, s => s.IsPassenger)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<Transportation, GetTransportationByIdResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.TransportationName, s => s.TransportationName)
           .Map(d => d.TransportationCode, s => s.TransportationCode)
           .Map(d => d.IsPassenger, s => s.IsPassenger)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<Transportation, GetTransportationByNameResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.TransportationName, s => s.TransportationName)
           .Map(d => d.TransportationCode, s => s.TransportationCode)
           .Map(d => d.IsPassenger, s => s.IsPassenger)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<Transportation, GetTransportationByCodeResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.TransportationName, s => s.TransportationName)
           .Map(d => d.TransportationCode, s => s.TransportationCode)
           .Map(d => d.IsPassenger, s => s.IsPassenger)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<Transportation, GetsActiveTransportationResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.TransportationName, s => s.TransportationName)
           .Map(d => d.TransportationCode, s => s.TransportationCode)
           .Map(d => d.IsPassenger, s => s.IsPassenger)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<Transportation, GetsFilteredTransportationResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.TransportationName, s => s.TransportationName)
           .Map(d => d.TransportationCode, s => s.TransportationCode)
           .Map(d => d.IsPassenger, s => s.IsPassenger)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.IsActive, s => s.IsActive)
           ;
    }
}