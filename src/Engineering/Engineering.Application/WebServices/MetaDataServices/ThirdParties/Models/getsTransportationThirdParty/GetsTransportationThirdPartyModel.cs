namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsTransportationThirdParty;

public record GetsTransportationThirdPartyModel()
{
    public long Id { get; set; }
    public bool? IsIndividual { get; set; }
    public long? UserId { get; set; }
    public string? FullName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Nickname { get; set; }
    public string? DefaultPhoneNo { get; set; }
    public string? IdentityNo { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? Created { get; set; }
    public TransportationLegalModel? Legal { get; set; }
    public TransportationAddressModel? Address { get; set; }
    public Guid? PreferentialReferenceCode { get; set; }
}

public record TransportationLegalModel(
    long? id,
    string? CompanyName,
    string? RegistrationNo
    );

public record TransportationAddressModel(
    long? id,
    string? Title,
    string? Address,
    long? CityId,
    string? City,
    string? PostalCode
    );