namespace Engineering.Application.Services.TransportationContractors.Contracts.CreateTransportationContractor;

public record CreateContractorPersonnelModel(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string IdentityNo,
    string? Description,
    string? CertificateNumber,
    long? LagacyId,
    CreateContractorPersonnelAddressModel? PersonnelAddress
);
