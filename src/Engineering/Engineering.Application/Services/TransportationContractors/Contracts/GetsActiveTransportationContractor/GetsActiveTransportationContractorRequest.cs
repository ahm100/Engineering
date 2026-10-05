
using Engineering.ClientSdk.Enums;

namespace Engineering.Application.Services.TransportationContractors.Contracts.GetsActiveTransportationContractor;

public record GetsActiveTransportationContractorRequest(
    List<long>? Ids,
    List<long>? ThirdPartyIds,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    List<DeliveryMethod>? DeliveryMethods,
    List<DeliveryType>? DeliveryTypes,
    List<TransportationContractorCalculateType>? Types,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
