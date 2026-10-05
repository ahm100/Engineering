using Engineering.Application.Services.Trips.Models.GetByCode;
using Engineering.Application.Services.Trips.Models.GetById;
using Engineering.Application.Services.Trips.Models.GetByName;
using Engineering.Application.Services.Trips.Models.GetsActive;
using Engineering.Application.Services.Trips.Models.GetsFiltered;
using Engineering.Application.Services.Trips.Models.GetsTripExcelExporter;
using Engineering.Domain.Entities.Trips;

namespace Engineering.Application.Mappers.Trips;

public class TripsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Trip, GetsTripExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.TripName, s => s.TripName)
           .Map(d => d.TripCode, s => s.TripCode)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<Trip, GetTripByIdResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.TripName, s => s.TripName)
           .Map(d => d.TripCode, s => s.TripCode)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<Trip, GetTripByNameResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.TripName, s => s.TripName)
           .Map(d => d.TripCode, s => s.TripCode)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<Trip, GetTripByCodeResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.TripName, s => s.TripName)
           .Map(d => d.TripCode, s => s.TripCode)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<Trip, GetsActiveTripResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.TripName, s => s.TripName)
           .Map(d => d.TripCode, s => s.TripCode)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<Trip, GetsFilteredTripResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.TripName, s => s.TripName)
           .Map(d => d.TripCode, s => s.TripCode)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.IsActive, s => s.IsActive)
           ;
    }
}