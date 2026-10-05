using Engineering.ClientSdk.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.UpdateFreeCargosTransportInfo;

public record UpdateFreeCargosTransportInfoRequest(
    long CargoId,
    List<long>? PalletIds,
    long? TransportationContractorId,
    DeliveryMethod? DeliveryMethod,
    DeliveryType? DeliveryType,
    DateTime? PostageDate,
    PackingShippingType? PackingShippingType,
    string? VehicleName,
    string? NumberPlate,
    string? Driver,
    string? DriverPhoneNumber
     ) : IHttpRequest;
