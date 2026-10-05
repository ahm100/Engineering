namespace Engineering.Application.Services.TransportationRequests.Models.CalculatePriceOfTransport;

public record CalculatePriceOfTransportResponse(
    long Id,
    decimal ProductPrice,
    decimal InsurancePrice,
    decimal ServicePrice,
    decimal PriceWeight,
    decimal TotalTransferPrice,
    decimal TaxPrice
    );
