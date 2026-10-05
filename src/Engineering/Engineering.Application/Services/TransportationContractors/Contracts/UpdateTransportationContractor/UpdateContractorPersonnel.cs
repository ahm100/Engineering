namespace Engineering.Application.Services.TransportationContractors.Contracts.UpdateTransportationContractor;

public record UpdateContractorPersonnelModel(
    long? Id,
    long? ThirdPartyId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string IdentityNo,
    string? Description,
    string? CertificateNumber,
    bool? IsActive,
    bool? IsDeleted,
    long? LegacyId,
    UpdateContractorPersonnelAddressModel? PersonnelAddress
);
