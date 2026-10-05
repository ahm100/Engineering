namespace Engineering.Application.Services.TransportationContractors.Contracts.UpdateTransportationContractor;

public record UpdateContractorInsuranceModel(
    long Id,
    decimal MinProductPrice,
    decimal MaxProductPrice,
    decimal FixedPrice,
    decimal? Multiplication,
    decimal? Division,
    decimal? Subtraction,
    decimal? Addition
);
