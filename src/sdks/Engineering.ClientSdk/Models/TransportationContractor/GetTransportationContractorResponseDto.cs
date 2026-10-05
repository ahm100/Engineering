using Engineering.ClientSdk.Enums;

namespace Engineering.ClientSdk.Models.TransportationContractor;

public class GetTransportationContractorResponseDto
{
    public List<GetsFilteredTransportationContractorResponseModel> Data { get; set; }
    public int RowCount { get; set; }
}

public record GetsFilteredTransportationContractorResponseModel
{
    public long Id { get; set; }
    public bool? IsIndivisual { get; set; }
    public string? IsIndivisualTitle => IsIndivisual == true ? "حقیقی" : "حقوقی";
    public long? LegacyId { get; set; }
    public long? ThirdPartyId { get; set; }
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? FullName => FirstName + " " + LastName;
    public string? PhoneNumber { get; set; }
    public string? IdentityNo { get; set; }
    public DateTime? StartOfContract { get; set; }
    public DateTime? EndOfContract { get; set; }
    public string? Description { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
    public DateTime? Created { get; set; }
    public long? LegalId { get; set; }
    public string? CompanyName { get; set; }
    public string? RegisterationNo { get; set; }
    public long? AddressId { get; set; }
    public long? CityId { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; } = string.Empty;
    public string? Title { get; set; } = string.Empty;
    public string? PostalCode { get; set; }
    public DeliveryMethod[]? DeliveryMethod { get; set; }
    public DeliveryType[]? DeliveryType { get; set; }
    public TransportationContractorCalculateType? Type { get; set; }
    public decimal? PercentageValue { get; set; }
    public decimal? FixedNumber { get; set; }
    public string? FirstPrefix { get; set; }
    public long? SecondPrefix { get; set; }
    public List<GetFilteredcontractorPriceWeightModel>? PriceWeights { get; set; }
    public List<GetsFilteredcontractorPersonnelModel>? Personnels { get; set; }
    public List<string>? TransportationContractorDocumentUrls { get; set; }
}

public record GetsFilteredcontractorPersonnelModel
{
    public long? Id { get; set; }
    public bool? IsIndivisual { get; set; }
    public string? IsIndivisualTitle => IsIndivisual == true ? "حقیقی" : "حقوقی";
    public long? ThirdPartyId { get; set; }
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? IdentityNo { get; set; }
    public string? Description { get; set; }
    public long? AddressId { get; set; }
    public long? CityId { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; } = string.Empty;
    public string? Title { get; set; } = string.Empty;
}

public record GetFilteredcontractorPriceWeightModel
{
    public long? Id { get; set; }
    public decimal? UntilWeight { get; set; }
    public bool? IsFixed { get; set; }
    public decimal? Price { get; set; }
}
