namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.CreateTransportationContractorPersonnel;

public record CreateTransportationContractorPersonnelRequest(
    long TransportationContractorId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string IdentityNo,
    string? Description,
    string? CertificateNumber,
    long? LagacyId,
    CreatePersonnelAddressModel? PersonnelAddress,
    List<long>? PersonnelMachines
    ) : IHttpRequest;

public record CreatePersonnelAddressModel(
    long CityId,
    string Address,
    string Title
);
