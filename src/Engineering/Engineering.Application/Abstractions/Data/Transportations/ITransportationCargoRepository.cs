using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredTransportationCargo;
using Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoById;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;
using Gita.Backend.Shared.Domain.Enums.SaleChannels;

namespace Engineering.Application.Abstractions.Data.Transportations;

public interface ITransportationCargoRepository : IBaseRepository<TransportationCargo>
{
    Task<List<TransportationCargo>?> GetByIds(List<long> ids, CancellationToken cancellationToken);

    Task<List<TransportationCargo>?> GetByPalletIds(List<long> ids, CancellationToken cancellationToken);

    Task<TransportationCargo?> GetById(long id, CancellationToken cancellationToken);

    Task<GetTransportationCargoByIdResponse?> GetCargoById(long id, CT ct);

    Task<(List<GetsFilteredTransportationCargoResponseModel> Data, int RowCount)> GetsFilteredTransportationCargo(
        List<long>? ids,
        List<long>? contractorIds,
        List<long>? transportationRequestIds,
        List<long>? packingIds,
        List<DeliveryMethod>? deliveryMethods,
        List<DeliveryType>? deliveryTypes,
        List<PackingShippingType>? packingShippingTypes,
        List<TransportationRequestStatus>? transportationRequestStatus,
        List<SalesChannelType>? salesChannelTypes,
        PalletTransportStatus? palletTransportStatus,
        List<long>? thirdPartyIds,
        List<long>? productIds,
        List<long>? warehouseIds,
        List<long>? destWarehouseIds,
        List<long>? cityIds,
        long? creatorId,
        long? thirdPartyId,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        long? companyId,
        bool? haveShippingType,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<bool?> IsDuplicateWithPackingIds(List<long>? packingIds, CT ct);
}