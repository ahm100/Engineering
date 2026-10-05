namespace Engineering.Application.Services.TransportationContractors.Contracts.CreateTransportationContractor;

public record CreateContractorPriceWeightModel(
    decimal UntilWeight,
    decimal Price,
    bool IsFixed
);
