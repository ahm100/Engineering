using Engineering.ClientSdk.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.UpdateAfterCargoDeclaration;

public record UpdateAfterCargoDeclarationRequest(
    long CargoId,
    long? TransportationContractorId,
    DeliveryMethod? DeliveryMethod,
    DeliveryType? DeliveryType,
    DateTime? PostageDate,
    Engineering.ClientSdk.Enums.PackingShippingType? PackingShippingType,
    string? VehicleName,
    string? NumberPlate,
    string? Driver,
    string? DriverPhoneNumber
     ) : IHttpRequest;
