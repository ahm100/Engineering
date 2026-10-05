using Engineering.ClientSdk.Enums;

namespace Engineering.Application.Services.TransportationContractors.Contracts.GetsFilteredTransportationContractor;

public record GetsFilteredTransportationContractorRequest(
    List<long>? Ids,
    List<long>? ThirdPartyIds,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    List<DeliveryMethod>? DeliveryMethods,
    List<DeliveryType>? DeliveryTypes,
    List<TransportationContractorCalculateType>? Types,
    PackingShippingType? PackingShippingType,
    bool? IsActive,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;