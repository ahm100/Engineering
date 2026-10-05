using Engineering.ClientSdk.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

[NotMapped]
public class ViewPackingShippingDetail : AuditableEntity<ViewPackingShippingDetail>
{
    public long? TransportationContractorId { get; private set; }

    public DeliveryMethod? DeliveryMethod { get; private set; }

    public DeliveryType? DeliveryType { get; private set; }

    public long? SourceCountry { get; private set; }

    public long? DestinationCountry { get; private set; }

    public string? VehicleName { get; private set; }

    public string? TravelNumber { get; private set; }

    public string? PartOfLeading { get; private set; }

    public string? PartOfDischarge { get; private set; }

    public DateTime? DepartureDate { get; private set; }

    public string? NumberPlate { get; private set; }

    public string? Driver { get; private set; }

    public string? DriverPhoneNumber { get; private set; }

    public PackingShippingType? PackingShippingType { get; private set; }

    public long PackingId { get; set; }
    [ForeignKey("PackingId")]
    public virtual ViewPacking Packing { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public ViewPackingShippingDetail()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}