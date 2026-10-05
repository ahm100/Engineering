using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Application.Services.TransportationRequests.Models.CreateWarehouseTransportation;

public record CreateWarehouseTransportationRequest(
    List<long> PackingIds,
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

public class ShippingCostsAndPriceWeightsDto
{
    public List<ShippingCost>? ShippingCosts { get; set; }
    public List<TransportationContractorPriceWeight>? PriceWeights { get; set; }
}