using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestExcelExporter;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Mappers.RequestMachineries;

public class TransportationRequestModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        config.NewConfig<TransportationRequest, GetsTransportationRequestExcelExporterResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.TransportationRequestStatusTitle, s => s.TransportationRequestStatus.GetEnumDescription())
           .Map(d => d.StartDate, s => TimeCalculator.ConvertToShamsi(s.StartDate))
           .Map(d => d.EndDate, s => TimeCalculator.ConvertToShamsi(s.EndDate))
           .Map(d => d.TransportationName, s => s.Transportation.TransportationName)
           .Map(d => d.IsPassenger, s => s.Transportation.IsPassenger)
           .Map(d => d.TripName, s => s.Trip.TripName)
           .Map(d => d.BillOfLadingName, s => s.BillOfLading == null ? "" : s.BillOfLading.BillOfLadingName)
           .Map(d => d.RequestById, s => s.CreatorId)
           .Map(d => d.Price, s => s.Price)
           .Map(d => d.CurrencyUnitId, s => s.CurrencyUnitId)
           .Map(d => d.ManagerDescription, s => s.ManagerDescription)
           .Map(d => d.StartingCityId, s => s.StartingCityId)
           .Map(d => d.DestinationCityId, s => s.DestinationCityId)
           .Map(d => d.CarID, s => s.CarID)
           .Map(d => d.ConfirmUserId, s => s.ConfirmUserId)
           .Map(d => d.ConfirmDateShamsi, s => TimeCalculator.ConvertToShamsi(s.ConfirmDate))
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.TransportationCostCategoryId, s => s.CostCategoryId)
           .Map(d => d.TransportationCostGroupId, s => s.CostGroupId)
           .Map(d => d.RequestNumber, s => s.RequestNumber)
           ;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8604 // Possible null reference argument.

    }
}
