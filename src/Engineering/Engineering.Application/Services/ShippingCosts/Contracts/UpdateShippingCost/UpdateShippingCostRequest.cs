namespace Engineering.Application.Services.ShippingCosts.Contracts.UpdateShippingCost;

public record UpdateShippingCostRequest(
    long Id,
    long TransportationContractorId,
    long MachineTypeId,
    long? SourceCityId,
    long? DestinationCityId,
    long? RegionId,
    int? Count,
    decimal? LoadWeight,
    decimal Price,
    decimal? Tax,
    bool IsActive,
    string? Description,
    DateTime? FromDate,
    DateTime? ToDate,
    long? ThirdPartyId,
    long? ThirdPartyCompanyId,
    decimal? Longitude,
    decimal? Latitude,
    long? LegacyId
    ) : IHttpRequest;