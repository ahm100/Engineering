namespace Engineering.Application.Services.TransportationContractors.Contracts.UpdateTransportationContractor;

public record UpdateContractorPersonnelAddressModel(
    long? Id,
    long CityId,
    string Address,
    string Title,
    bool? IsDeleted
);
