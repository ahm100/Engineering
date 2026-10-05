using Engineering.ClientSdk.Enums;

namespace Engineering.Application.Services.TransportationContractors.Contracts.GetsActiveTransportationContractor;

public record GetsActiveTransportationContractorResponseModel
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
    public string? StartOfContractShamsi => TimeCalculator.ConvertToShamsi(StartOfContract);
    public DateTime? EndOfContract { get; set; }
    public string? EndOfContractShamsi => TimeCalculator.ConvertToShamsi(EndOfContract);
    public string? Description { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
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
    public string? TypeTitle => Type?.GetEnumDescription();
    public decimal? PercentageValue { get; set; }
    public decimal? FixedNumber { get; set; }
    public string? FirstPrefix { get; set; }
    public long? SecondPrefix { get; set; }
    public decimal? TaxPercent { get; set; }
    public decimal? ServicePrice { get; set; }
    public List<GetActiveContractorInsuranceModel>? Insurances { get; set; }
    public List<GetActivecontractorPriceWeightModel>? PriceWeights { get; set; }
    public List<GetsActivecontractorPersonnelModel>? Personnels { get; set; }
    public List<GetsActivecontractorManagerModel>? Managers { get; set; }
    public List<string>? TransportationContractorDocumentUrls { get; set; }
    public string? ContractorTitle { get; set; }
}

public record GetsActivecontractorPersonnelModel
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

public record GetsActivecontractorManagerModel
{
    public long? Id { get; set; }
    public long? ThirdPartyId { get; set; }
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? FullName => FirstName + " " + LastName;
    public string? PhoneNumber { get; set; }
    public string? OrganizationCode { get; set; }
}

public record GetActivecontractorPriceWeightModel
{
    public long? Id { get; set; }
    public decimal? UntilWeight { get; set; }
    public bool? IsFixed { get; set; }
    public decimal? Price { get; set; }
}

public record GetActiveContractorInsuranceModel
{
    public long? Id { get; set; }
    public decimal? MinProductPrice { get; set; }
    public decimal? MaxProductPrice { get; set; }
    public decimal? FixedPrice { get; set; }
    public decimal? Multiplication { get; set; }
    public decimal? Division { get; set; }
    public decimal? Subtraction { get; set; }
    public decimal? Addition { get; set; }
}