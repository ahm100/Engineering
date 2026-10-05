using Engineering.Application.Services.TransportationRequests.Models.GetPackingLogesticDetail;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationCargoPallet;
using Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoPallet;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;
using Gita.Backend.Shared.Domain.Enums.SaleChannels;

namespace Engineering.Application.Abstractions.Data.Transportations;

public interface ITransportationCargoPalletRepository : IBaseRepository<TransportationCargoPallet>
{
    Task<List<TransportationCargoPallet>?> GetByIds(List<long> ids, CancellationToken cancellationToken);

    Task<TransportationCargoPallet?> GetById(long id, CancellationToken cancellationToken);

    Task<GetTransportationCargoPalletResponse?> GetPalletById(long id, CT ct);

    Task<(List<GetsTransportationCargoPalletResponseModel> Data, int RowCount)> GetsTransportationCargo(
        List<long>? ids,
        List<long>? contractorIds,
        List<long>? cargoIds,
        List<long>? transportationRequestIds,
        List<long>? packingIds,
        List<DeliveryMethod>? deliveryMethods,
        List<DeliveryType>? deliveryTypes,
        List<PackingShippingType>? packingShippingTypes,
        List<TransportationRequestStatus>? transportationRequestStatus,
        List<SalesChannelType>? salesChannelTypes,
        List<long>? thirdPartyIds,
        List<long>? productIds,
        List<long>? warehouseIds,
        List<long>? desWarehouseIds,
        List<long>? cityIds,
        long? creatorId,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        long? companyId,
        bool? haveShippingType,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsTransportationCargoPalletResponseModel> Data, int RowCount)> GetsTransportationCargoWithoutContractor(
        List<long>? ids,
        List<long>? cargoIds,
        List<long>? transportationRequestIds,
        List<long>? packingIds,
        List<DeliveryMethod>? deliveryMethods,
        List<DeliveryType>? deliveryTypes,
        List<PackingShippingType>? packingShippingTypes,
        List<TransportationRequestStatus>? transportationRequestStatus,
        List<SalesChannelType>? salesChannelTypes,
        List<long>? thirdPartyIds,
        List<long>? productIds,
        List<long>? warehouseIds,
        List<long>? desWarehouseIds,
        List<long>? cityIds,
        long? creatorId,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        long? companyId,
        bool? haveShippingType,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<TransportationCargoPallet>?> GetTransportationPallets(
         List<long>? ids,
         CT ct);

    Task<List<TransportationCargoPallet>?> GetTransportationPalletsByPackingIds(
        List<long>? ids,
        CT ct);

    Task<GetPackingLogesticDetailResponse?> GetLogesticByPackingId(long id, CT ct);
}