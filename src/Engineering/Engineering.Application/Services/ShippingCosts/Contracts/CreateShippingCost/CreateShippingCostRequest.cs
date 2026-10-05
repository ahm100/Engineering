namespace Engineering.Application.Services.ShippingCosts.Contracts.CreateShippingCost;

public record CreateShippingCostRequest(
        long TransportationContractorId,
        List<CreateShippingCostModel> Shippingcosts
    ) : IHttpRequest;

public record CreateShippingCostModel(
        long MachineTypeId,
        long? SourceCityId,
        long? DestinationCityId,
        long? RegionId,
        int? Count,
        decimal? LoadWeight,
        decimal Price,
        decimal? Tax,
        string? Description,
        DateTime? FromDate,
        DateTime? ToDate,
        long? ThirdPartyId,
        long? ThirdPartyCompanyId,
        decimal? Longitude,
        decimal? Latitude,
        long? LegacyId);