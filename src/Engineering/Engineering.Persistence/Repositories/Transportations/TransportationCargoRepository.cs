using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredTransportationCargo;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationCargoPallet;
using Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoById;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;
using Engineering.Domain.Entities.Transportations.Enums;
using Gita.Backend.Shared.Domain.Enums.SaleChannels;
using TransportationCargo = Engineering.Domain.Entities.Transportations.TransportationCargo;

namespace Engineering.Persistence.Repositories.Transportations;

public class TransportationCargoRepository : BaseRepository<EngineeringDBContext, TransportationCargo>, ITransportationCargoRepository
{
    public TransportationCargoRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<TransportationCargo?> GetById(long id, CT cancellationToken)
    {
        return await DbSet
            .Include(x => x.TransportationCargoPallets)
                .ThenInclude(z => z.PackingPallet)
            .Include(x => x.Packing)
            .Include(x => x.TransportationCargoDocuments)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<TransportationCargo>?> GetByIds(List<long> ids, CT cancellationToken)
    {
        return await DbSet
            .Include(x => x.Packing)
            .Include(x => x.TransportationCargoDocuments)
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TransportationCargo>?> GetByPalletIds(List<long> ids, CT cancellationToken)
    {
        return await DbSet
            .Include(x => x.Packing)
            .Include(x => x.TransportationCargoDocuments)
            .Where(x => x.TransportationCargoPallets.Any(z => ids.Contains(z.Id)))
            .ToListAsync(cancellationToken);
    }

    public async Task<GetTransportationCargoByIdResponse?> GetCargoById(long id, CT ct)
    {
        var item = await DbSet
        .Where(t => t.Id == id)
        .Select(x => new GetTransportationCargoByIdResponse
        {
            Id = x.Id,
            CargoCreated = x.Created,
            PackingNumber = x.Packing != null ? x.Packing.RequestNumber : null,
            PackingId = x.PackingId,
            Status = x.Packing != null ? x.Packing.Status : null,
            SalesChannelType = x.Packing != null ? x.Packing.SalesChannelType : null,
            ThirdPartyId = x.ThirdPartyId,
            ThirdPartyFullName = x.ThirdParty != null ? x.ThirdParty.FirstName + " " + x.ThirdParty.LastName : null,
            ThirdParty = x.ThirdParty != null ? x.ThirdPartyName : null,
            DeliveryDate = x.Packing != null ? x.Packing.DeliveryDate : null,
            SecurityConfirm = x.Packing != null ? x.Packing.SecurityConfirm : false,
            SecurityConfirmDate = x.Packing != null ? x.Packing.SecurityConfirmDate : null,
            PalletNumbers = x.TransportationCargoPallets.Select(x => x.PalletNumber),
            TransportationRequestNumbers = x.TransportationCargoPallets
            .Where(x => x.TransportationRequest != null && x.TransportationRequest.RequestNumber != null)
            .Select(x => x.TransportationRequest!.RequestNumber!.Value.ToString()),
            CreatorId = x.CreatorId,
            CargoSecurityConfirm = x.SecurityConfirm,
            CargoSecurityConfirmDate = x.SecurityConfirmDate,
            CargoDocs = x.TransportationCargoDocuments.Select(x =>
            new CargoDcoumentsDto
            {
                Id = x.Id,
                Url = x.Url,
            }),
            PalletTransportStatus =
                x.TransportationCargoPallets.All(p => p.TransportationRequest != null)
                    ? PalletTransportStatus.AllHaveTransport
                    : x.TransportationCargoPallets.All(p => p.TransportationRequest == null)
                        ? PalletTransportStatus.AllDontHaveTransport
                        : PalletTransportStatus.SomeDontHaveTransport
        }).FirstOrDefaultAsync(ct);

        return item;
    }

    public async Task<(List<GetsFilteredTransportationCargoResponseModel> Data, int RowCount)> GetsFilteredTransportationCargo(
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
        CT ct)
    {
        var query = DbSet
        .Where(t =>
            (haveShippingType == null ||
                (haveShippingType == true && t.TransportationCargoPallets.Any(x => x.PackingShippingType != null)) ||
                (haveShippingType == false && t.TransportationCargoPallets.Any(x => x.PackingShippingType == null))) &&

            (contractorIds == null || contractorIds.Count == 0 ||
            (t.TransportationCargoPallets != null && t.TransportationCargoPallets.Any(x =>
            x.TransportationContractorId != null && contractorIds.Contains(x.TransportationContractorId.Value)))) &&

            (fromDate == null ||
            (t.TransportationCargoPallets != null && t.TransportationCargoPallets.Any(x => x.PostageDate != null &&
            x.PostageDate.Value.Date >= fromDate.Value.Date))) &&

            (toDate == null ||
            (t.TransportationCargoPallets != null && t.TransportationCargoPallets.Any(x => x.PostageDate != null &&
            x.PostageDate.Value.Date <= toDate.Value.Date))) &&

            (deliveryMethods == null || deliveryMethods.Count == 0 ||
            (t.TransportationCargoPallets != null && t.TransportationCargoPallets.Any(x => x.DeliveryMethod != null &&
            deliveryMethods.Contains(x.DeliveryMethod.Value)))) &&

            (deliveryTypes == null || deliveryTypes.Count == 0 ||
            (t.TransportationCargoPallets != null && t.TransportationCargoPallets.Any(x => x.DeliveryType != null &&
            deliveryTypes.Contains(x.DeliveryType.Value)))) &&

            (packingShippingTypes == null || packingShippingTypes.Count == 0 ||
            (t.TransportationCargoPallets != null && t.TransportationCargoPallets.Any(x => x.PackingShippingType != null &&
            packingShippingTypes.Contains(x.PackingShippingType.Value)))) &&

            (ids == null || ids.Count == 0 || ids.Contains(t.Id)) &&

            (thirdPartyIds == null || thirdPartyIds.Count == 0 || (t.ThirdPartyId != null &&
            thirdPartyIds.Contains(t.ThirdPartyId.Value))) &&

            (transportationRequestStatus == null || transportationRequestStatus.Count == 0 ||
            t.TransportationCargoPallets.Any(z => z.TransportationRequest != null &&
            transportationRequestStatus.Contains(z.TransportationRequest.TransportationRequestStatus))) &&

            (transportationRequestIds == null || transportationRequestIds.Count == 0 ||
            t.TransportationCargoPallets.Any(z => z.TransportationRequest != null &&
            transportationRequestIds.Contains(z.TransportationRequestId!.Value))) &&

            (packingIds == null || packingIds.Count == 0 || packingIds.Contains(t.PackingId)) &&

            (companyId == null || t.Packing.CompanyId == companyId) &&

            (productIds == null || productIds.Count == 0 || t.TransportationCargoPallets.Any(z =>
            z.TransportationRequestWarehouses.Any(z => z.PackingProduct != null && productIds.Contains(z.PackingProduct.ProductId)))) &&

            (warehouseIds == null || warehouseIds.Count == 0 || t.TransportationCargoPallets.Any(z =>
            z.TransportationRequestWarehouses.Any(z => z.PackingSourceAddress != null &&
            z.PackingSourceAddress.WarehouseId != null && warehouseIds.Contains(z.PackingSourceAddress.WarehouseId.Value)))) &&

            (destWarehouseIds == null || destWarehouseIds.Count == 0 || t.TransportationCargoPallets.Any(z =>
            z.TransportationRequestWarehouses.Any(z => z.PackingDestinationAddress != null &&
            z.PackingDestinationAddress.WarehouseId != null && destWarehouseIds.Contains(z.PackingDestinationAddress.WarehouseId.Value)))) &&

            (cityIds == null || cityIds.Count == 0 || t.TransportationCargoPallets.Any(z =>
            z.TransportationRequestWarehouses.Any(z => z.PackingDestinationAddress != null &&
            z.PackingDestinationAddress.CityId != null && cityIds.Contains(z.PackingDestinationAddress.CityId.Value)))) &&

            (salesChannelTypes == null || salesChannelTypes.Count == 0 || (t.Packing != null && t.Packing.SalesChannelType != null &&
            salesChannelTypes.Contains(t.Packing.SalesChannelType.Value))) &&

            (creatorId == null || t.CreatorId == creatorId) &&
            (thirdPartyId == null || t.TransportationCargoPallets.Any(z => z.TransportationContractor != null &&
                z.TransportationContractor.ContractorManagers.Any(c => c.ThirdPartyId == thirdPartyId))) &&

            (string.IsNullOrEmpty(filterData) ||
            t.TransportationCargoPallets.Any(x => x.TransportationRequest != null && x.TransportationRequest.RequestNumber != null &&
             EF.Functions.Like(x.TransportationRequest.RequestNumber!.Value.ToString(), filterData.MakeLikePattern())) ||
            (t.Packing != null && EF.Functions.Like(t.Packing.RequestNumber.ToString(), filterData.MakeLikePattern()))))

        .Select(x => new GetsFilteredTransportationCargoResponseModel
        {
            Id = x.Id,
            CargoCreated = x.Created,
            PackingNumber = x.Packing != null ? x.Packing.RequestNumber : null,
            PackingId = x.PackingId,
            Status = x.Packing != null ? x.Packing.Status : null,
            SalesChannelType = x.Packing != null ? x.Packing.SalesChannelType : null,
            ThirdPartyId = x.ThirdPartyId,
            ThirdPartyFullName = x.ThirdParty != null ? x.ThirdParty.FirstName + " " + x.ThirdParty.LastName : null,
            ThirdParty = x.ThirdParty != null ? x.ThirdPartyName : null,
            DeliveryDate = x.Packing != null ? x.Packing.DeliveryDate : null,

            SecurityConfirm = x.TransportationCargoPallets != null &&
                x.TransportationCargoPallets.Any(z => z.PackingPallet.PalletPermitStatus != PalletPermitStatus.PermitConfirmed)
                ? false : true,
            SecurityConfirmDate = x.TransportationCargoPallets != null &&
                x.TransportationCargoPallets.Any(z => z.PackingPallet.PalletPermitStatus != PalletPermitStatus.PermitConfirmed) ?
                null : x.TransportationCargoPallets!.First().PackingPallet.PermitDate,

            PalletNumbers = x.TransportationCargoPallets.Select(x => x.PalletNumber),
            TransportationRequestNumbers = x.TransportationCargoPallets
            .Where(x => x.TransportationRequest != null && x.TransportationRequest.RequestNumber != null)
            .Select(x => x.TransportationRequest!.RequestNumber!.Value.ToString()),
            CreatorId = x.CreatorId,
            CargoSecurityConfirm = x.SecurityConfirm,
            CargoSecurityConfirmDate = x.SecurityConfirmDate,
            CargoDocs = x.TransportationCargoDocuments.Select(x =>
            new CargoDcoumentsDto
            {
                Id = x.Id,
                Url = x.Url,
            }),
            PalletTransportStatus =
                x.TransportationCargoPallets.All(p => p.TransportationRequest != null)
                    ? PalletTransportStatus.AllHaveTransport
                    : x.TransportationCargoPallets.All(p => p.TransportationRequest == null)
                        ? PalletTransportStatus.AllDontHaveTransport
                        : PalletTransportStatus.SomeDontHaveTransport,

            SourceAddresses = x.TransportationCargoPallets
            .Select(z => z.PackingSourceAddress.Warehouse.Name ?? z.PackingSourceAddress.Address).Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList(),

            DesAddresses = x.TransportationCargoPallets
            .Select(z => z.PackingDestinationAddress.Warehouse.Name ?? z.PackingDestinationAddress.Address).Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList(),

            PermitNumbers = x.TransportationCargoPallets.Select(x => x.PackingPallet.PermitNumber).ToList()
        });

        if (palletTransportStatus != null)
        {
            if (palletTransportStatus == PalletTransportStatus.AllHaveTransport)
            {
                query = query.Where(x => x.PalletTransportStatus == PalletTransportStatus.AllHaveTransport);
            }
            else if (palletTransportStatus == PalletTransportStatus.AllDontHaveTransport)
            {
                query = query.Where(x => x.PalletTransportStatus == PalletTransportStatus.AllDontHaveTransport);
            }
            else if (palletTransportStatus == PalletTransportStatus.SomeDontHaveTransport)
            {
                query = query.Where(x => x.PalletTransportStatus == PalletTransportStatus.SomeDontHaveTransport);
            }
        }

        query = query.OrderByDescending(t => t.CargoCreated);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<bool?> IsDuplicateWithPackingIds(List<long>? packingIds, CT ct)
    {
        var query = await DbSet.Where(x => x.IsDeleted == false &&
        (packingIds == null || packingIds.Contains(x.PackingId)))
            .AnyAsync(ct);
        return query;
    }
}