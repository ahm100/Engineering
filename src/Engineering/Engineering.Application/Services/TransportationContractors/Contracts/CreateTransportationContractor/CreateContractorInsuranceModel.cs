namespace Engineering.Application.Services.TransportationContractors.Contracts.CreateTransportationContractor;

public record CreateContractorInsuranceModel(
    decimal MinProductPrice,
    decimal MaxProductPrice,
    decimal FixedPrice,
    decimal? Multiplication,
    decimal? Division,
    decimal? Subtraction,
    decimal? Addition
);
