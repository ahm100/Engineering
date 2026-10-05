namespace Engineering.Application.Services.TransportationContractors.Contracts.UpdateTransportationContractor;

public record UpdateTransportationContractorAddressModel(
    long? Id,
    long CityId,
    string Address,
    string Title,
    string? PostalCode,
    bool? IsDeleted
);
