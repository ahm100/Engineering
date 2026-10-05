using Engineering.Application.Extensions.PlateHelper;
using Engineering.ClientSdk.Enums;
using static Engineering.Application.Extensions.PlateHelper.PlateNumberHelper;

namespace Engineering.Application.Services.TransportationRequests.Models.GetPackingLogesticDetail;

public record GetPackingLogesticDetailResponse()
{
    public long Id { get; set; }
    public long? CargoId { get; set; }
    public long? TransportationContractorId { get; set; }
    public string? TransportationContractor { get; set; }
    public long? ThirdPartyId { get; set; }
    public string? ThirdParty { get; set; }
    public DateTime? PostageDate { get; set; }
    public string? PostageDateShamsi => TimeCalculator.ConvertToShamsi(PostageDate);
    public DeliveryMethod? DeliveryMethod { get; set; }
    public string? DeliveryMethodTitle => DeliveryMethod?.GetEnumDescription();
    public DeliveryType? DeliveryType { get; set; }
    public string? DeliveryTypeTitle => DeliveryType?.GetEnumDescription();
    public PackingShippingType? PackingShippingType { get; set; }
    public string? PackingShippingTypeTitle => PackingShippingType?.GetEnumDescription();
    public string? VehicleName { get; set; }
    public string? NumberPlate { get; set; }
    public LogesticNumberPlateDataModel? PlateModel => NumberPlate.ToPlateModel<LogesticNumberPlateDataModel>();
    public string? Driver { get; set; }
    public string? DriverPhoneNumber { get; set; }
}


public class LogesticNumberPlateDataModel : IPlateNumberModel
{
    public string? Part1 { get; set; } = string.Empty;
    public string? Part2 { get; set; } = string.Empty;
    public string? Part3 { get; set; } = string.Empty;
    public string? Letter { get; set; } = string.Empty;
}