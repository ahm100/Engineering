using Engineering.ClientSdk.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.AggregateWarehouseTransportation;

public record AggregateWarehouseTransportationRequest(
    List<CreateWarehouseTransportationDataModel> Cargos
     ) : IHttpRequest;

public record CreateWarehouseTransportationDataModel(
    List<long> PalletIds,
    long? MachineTypeId,
    long? DriverId,
    string? Driver,
    string? PhoneNumber,
    string? NumberPlate,
    decimal? Price,
    string? CertificateNumber,
    string? CarSpecifications,
    string? GlobalFreightNumber,
    string? Description,
    List<string>? DocumentUrls,
    long? TransportationContractorId,
    DeliveryMethod? DeliveryMethod,
    DeliveryType? DeliveryType,
    DateTime? PostageDate,
    PackingShippingType? PackingShippingType,
    string? VehicleName);
