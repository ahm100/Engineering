using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Transportations.Enums;
using Gita.Backend.Shared.Domain.Enums.SaleChannels;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsTransportationCargoWithoutContractor;

public record GetsTransportationCargoWithoutContractorRequest(
    List<long>? Ids,
    List<long>? CargoIds,
    List<long>? TransportationRequestIds,
    List<long>? PackingIds,
    List<DeliveryMethod>? DeliveryMethods,
    List<DeliveryType>? DeliveryTypes,
    List<PackingShippingType>? PackingShippingTypes,
    List<TransportationRequestStatus>? TransportationRequestStatus,
    List<SalesChannelType>? SalesChannelTypes,
    List<long>? ThirdPartyIds,
    List<long>? ProductIds,
    List<long>? WarehouseIds,
    List<long>? DesWarehouseIds,
    List<long>? CityIds,
    long? CreatorId,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    bool? HaveShippingType,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
