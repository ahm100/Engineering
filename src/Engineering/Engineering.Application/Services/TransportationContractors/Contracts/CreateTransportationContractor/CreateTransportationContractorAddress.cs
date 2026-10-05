namespace Engineering.Application.Services.TransportationContractors.Contracts.CreateTransportationContractor;

public record CreateTransportationContractorAddressModel(
    long CityId,
    string Address,
    string Title,
    string? PostalCode
);
