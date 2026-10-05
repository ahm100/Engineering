using Engineering.Domain.Entities.Synonyms.MetaData.Cities;
using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Synonyms.MetaData.Addresses;

[NotMapped]
public class ViewAddress : ActivateEntity<ViewAddress>
{
    private ViewAddress()
    {
    }

    public long? CityId { get; }
    public long? ThirdPartyId { get; }
    public string? AddressText { get; private set; }
    public string? ApartmentNo { get; private set; }
    public string? BuzzerNo { get; private set; }
    public string? FloorNo { get; private set; }
    public bool? IsDefault { get; private set; }
    public string? PostalCode { get; private set; }
    public string Title { get; private set; }
    public decimal? Latitude { get; private set; }
    public decimal? Longitude { get; private set; }
    public ViewCity? City { get; private set; }
    public ViewThirdParty ThirdParty { get; private set; }
}