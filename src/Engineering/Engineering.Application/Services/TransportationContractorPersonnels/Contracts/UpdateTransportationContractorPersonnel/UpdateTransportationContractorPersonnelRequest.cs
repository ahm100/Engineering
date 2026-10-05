namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.UpdateTransportationContractorPersonnel;

public record UpdateTransportationContractorPersonnelRequest(
    long Id,
    long ThirdPartyId,
    long TransportationContractorId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string IdentityNo,
    string? Description,
    string? CertificateNumber,
    long? LagacyId,
    bool IsActive,
    UpdatePersonnelAddressModel? PersonnelAddress,
    List<long>? PersonnelMachines
    ) : IHttpRequest;

public record UpdatePersonnelAddressModel(
    long? Id,
    long CityId,
    string Address,
    string Title,
    bool IsDeleted
);
