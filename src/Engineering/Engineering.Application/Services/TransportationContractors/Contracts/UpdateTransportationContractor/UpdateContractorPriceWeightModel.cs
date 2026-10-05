namespace Engineering.Application.Services.TransportationContractors.Contracts.UpdateTransportationContractor;

public record UpdateContractorPriceWeightModel(
    long Id,
    decimal UntilWeight,
    decimal Price,
    bool IsFixed
);
