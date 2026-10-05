using Engineering.ClientSdk.Enums;

namespace Engineering.ClientSdk.Models.TransportationContractor;

public class CreateWarehouseTransportationRequestDto
{
    public required List<long> PackingIds { get; set; }
    public long? TransportationContractorId { get; set; }
    public DeliveryMethod? DeliveryMethod { get; set; }
    public DeliveryType? DeliveryType { get; set; }
    public DateTime? PostageDate { get; set; }
    public PackingShippingType? PackingShippingType { get; set; }
    public string? VehicleName { get; set; }
    public string? NumberPlate { get; set; }
    public string? Driver { get; set; }
    public string? DriverPhoneNumber { get; set; }
}