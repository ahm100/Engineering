using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Services.TransportationRequests.Models.GetPackingLogesticDetail;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationCargoPallet;
using Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoPallet;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;
using Engineering.Domain.Entities.Transportations.Enums;
using Gita.Backend.Shared.Domain.Enums.SaleChannels;
using TransportationCargoPallet = Engineering.Domain.Entities.Transportations.TransportationCargoPallet;

namespace Engineering.Persistence.Repositories.Transportations;

public class TransportationCargoPalletRepository : BaseRepository<EngineeringDBContext, TransportationCargoPallet>, ITransportationCargoPalletRepository
{
    public TransportationCargoPalletRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<TransportationCargoPallet?> GetById(long id, CT cancellationToken)
    {
        return await DbSet
            .Include(x => x.TransportationCargo)
            .Include(x => x.TransportationRequest)
            .Include(x => x.PackingPallet)
            .Include(x => x.PackingDestinationAddress)
            .Include(x => x.PackingSourceAddress)
            .Include(x => x.ShippingCost)
            .Include(x => x.TransportationRequestWarehouses)
                .ThenInclude(x => x.PackingProduct)
                    .ThenInclude(x => x.DestinationPackingAddress)
            .Include(x => x.TransportationRequestWarehouses)
                .ThenInclude(x => x.PackingProduct)
                    .ThenInclude(x => x.SourcePackingAddress)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<GetTransportationCargoPalletResponse?> GetPalletById(long id, CT ct)
    {
        var item = await DbSet
        .Where(t => t.Id == id)
        .Select(x => new GetTransportationCargoPalletResponse
        {
            Id = x.Id,
            CargoId = x.TransportationCargo.Id,
            CargoCreated = x.TransportationCargo.Created,
            Created = x.Created,
            CreatorId = x.CreatorId,
            PackingNumber = x.TransportationCargo.Packing != null ? x.TransportationCargo.Packing.RequestNumber : null,
            PackingId = x.TransportationCargo.PackingId,
            Status = x.TransportationCargo.Packing != null ? x.TransportationCargo.Packing.Status : null,
            SalesChannelType = x.TransportationCargo.Packing != null ? x.TransportationCargo.Packing.SalesChannelType : null,
            ThirdPartyId = x.TransportationCargo.ThirdPartyId,
            ThirdPartyFullName = x.TransportationCargo.ThirdParty != null ? x.TransportationCargo.ThirdParty.FirstName + " " + x.TransportationCargo.ThirdParty.LastName : null,
            ThirdParty = x.TransportationCargo.ThirdParty != null ? x.TransportationCargo.ThirdPartyName : null,
            TransportationContractorId = x.TransportationContractorId,
            DeliveryMethod = x.DeliveryMethod,
            DeliveryType = x.DeliveryType,
            PackingShippingType = x.PackingShippingType,
            VehicleName = x.VehicleName,
            Driver = x.Driver,
            DriverPhoneNumber = x.DriverPhoneNumber,
            PostageDate = x.PostageDate,
            NumberPlate = x.NumberPlate,
            TransportationRequestId = x.TransportationRequest != null ? x.TransportationRequestId : null,
            TransportationRequestNumber = x.TransportationRequest != null ? x.TransportationRequest.RequestNumber : null,
            PackingPalletId = x.PackingPalletId,
            PalletNumber = x.PalletNumber,
            Weight = x.PackingPallet != null ? x.PackingPallet.Weight : null,
            Height = x.PackingPallet != null ? x.PackingPallet.Height : null,
            PalletPrice = x.Price,
            PalletTransferPrice = x.TransferPrice,
            PalletWeight = x.Weight,
            PalletQuantity = x.Quantity,
            PackagingSpecType = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.PackagingSpecType : null,
            PackagingSpecTitle = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Title : null,
            PackagingSpecQuantity = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Quantity : 0,
            PackagingSpecLength = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Length : null,
            PackagingSpecWidth = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Width : null,
            PackagingSpecHeight = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Height : null,
            PackagingSpecWeight = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Weight : null,
            PackagingSpecRatio = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Ratio : null,
            PackagingSpecMeasureUnitId = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null && x.PackingPallet.PackagingSpec.MeasureUnitId != null
                ? x.PackingPallet.PackagingSpec.MeasureUnitId : null,
            PackingSourceAddressId = x.PackingSourceAddress != null ? x.PackingSourceAddress.Id : null,
            PackingSourceAddressCityId = x.PackingSourceAddress != null && x.PackingSourceAddress.CityId != null ? x.PackingSourceAddress.CityId : null,
            PackingSourceAddressWarehouseId = x.PackingSourceAddress != null && x.PackingSourceAddress.Warehouse != null ? x.PackingSourceAddress.Warehouse.Id : null,
            PackingSourceAddressWarehouse = x.PackingSourceAddress != null && x.PackingSourceAddress.Warehouse != null ? x.PackingSourceAddress.Warehouse.Name : null,
            PackingSourceAddress = x.PackingSourceAddress != null ? x.PackingSourceAddress.Address : null,
            PackingDestinationAddressId = x.PackingDestinationAddress != null ? x.PackingDestinationAddress.Id : null,
            PackingDestinationAddressCityId = x.PackingDestinationAddress != null && x.PackingDestinationAddress.CityId != null ? x.PackingDestinationAddress.CityId : null,
            PackingDestinationAddressWarehouseId = x.PackingDestinationAddress != null && x.PackingDestinationAddress.Warehouse != null ? x.PackingDestinationAddress.Warehouse.Id : null,
            PackingDestinationAddressWarehouse = x.PackingDestinationAddress != null && x.PackingDestinationAddress.Warehouse != null ? x.PackingDestinationAddress.Warehouse.Name : null,
            PackingDestinationAddress = x.PackingDestinationAddress != null ? x.PackingDestinationAddress.Address : null,
            PackingDestinationPhoneNumber = x.PackingDestinationAddress != null ? x.PackingDestinationAddress.PhoneNumber : null,
            PackingDestinationPostalCode = x.PackingDestinationAddress != null ? x.PackingDestinationAddress.PostalCode : null,
            ShippingCostId = x.ShippingCost != null ? x.ShippingCost.Id : null,
            ShippingCostPrice = x.ShippingCost != null ? x.ShippingCost.Price : null,
            DeliveryDate = x.TransportationCargo.Packing != null ? x.TransportationCargo.Packing.DeliveryDate : null,
            SecurityConfirm = x.TransportationCargo.Packing != null ? x.TransportationCargo.Packing.SecurityConfirm : false,
            SecurityConfirmDate = x.TransportationCargo.Packing != null ? x.TransportationCargo.Packing.SecurityConfirmDate : null,
            CargoSecurityConfirm = x.TransportationCargo.SecurityConfirm,
            CargoSecurityConfirmDate = x.TransportationCargo.SecurityConfirmDate,
            CargoDocuments = x.TransportationCargo.TransportationCargoDocuments.Select(x =>
            new CargoDcoumentsDto
            {
                Id = x.Id,
                Url = x.Url,
            }).ToList(),
            PalletProducts = x.TransportationRequestWarehouses != null && x.TransportationRequestWarehouses.Any()
                ? x.TransportationRequestWarehouses.Where(w => !w.IsDeleted && w.Quantity > 0).Select(w => new TransportationRequestWarehouseByIdPalletDto
                {
                    Id = w.Id,
                    TransportationCargoPalletId = w.TransportationCargoPalletId,
                    PackingProductId = w.PackingProductId,
                    ProductId = w.PackingProduct != null ? w.PackingProduct.ProductId : null,
                    ProductName = w.PackingProduct != null ? w.PackingProduct.Product.Name : null,
                    ProductCode = w.PackingProduct != null ? w.PackingProduct.Product.Code : null,
                    PalletNumber = w.PalletNumber,
                    PackingSourceAddressId = w.PackingSourceAddress != null ? w.PackingSourceAddress.Id : null,
                    PackingSourceAddressCityId = w.PackingSourceAddress != null && w.PackingSourceAddress.CityId != null ? w.PackingSourceAddress.CityId : null,
                    PackingSourceAddressWarehouseId = w.PackingSourceAddress != null && w.PackingSourceAddress.Warehouse != null ? w.PackingSourceAddress.Warehouse.Id : null,
                    PackingSourceAddressWarehouse = w.PackingSourceAddress != null && w.PackingSourceAddress.Warehouse != null ? w.PackingSourceAddress.Warehouse.Name : null,
                    PackingSourceAddress = w.PackingSourceAddress != null ? w.PackingSourceAddress.Address : null,
                    PackingDestinationAddressId = w.PackingDestinationAddress != null ? w.PackingDestinationAddress.Id : null,
                    PackingDestinationAddressCityId = w.PackingDestinationAddress != null && w.PackingDestinationAddress.CityId != null ? w.PackingDestinationAddress.CityId : null,
                    PackingDestinationAddressWarehouseId = w.PackingDestinationAddress != null && w.PackingDestinationAddress.Warehouse != null ? w.PackingDestinationAddress.Warehouse.Id : null,
                    PackingDestinationAddressWarehouse = w.PackingDestinationAddress != null && w.PackingDestinationAddress.Warehouse != null ? w.PackingDestinationAddress.Warehouse.Name : null,
                    PackingDestinationAddress = w.PackingDestinationAddress != null ? w.PackingDestinationAddress.Address : null,
                    PackingDestinationPhoneNumber = w.PackingDestinationAddress != null ? w.PackingDestinationAddress.PhoneNumber : null,
                    PackingDestinationPostalCode = w.PackingDestinationAddress != null ? w.PackingDestinationAddress.PostalCode : null,
                    Price = w.Price,
                    Quantity = w.Quantity,
                    Created = w.Created,
                }).ToList()
                : new List<TransportationRequestWarehouseByIdPalletDto>()
        }).FirstOrDefaultAsync(ct);

        return item;
    }

    public async Task<List<TransportationCargoPallet>?> GetByIds(List<long> ids, CT cancellationToken)
    {
        return await DbSet
            .Include(x => x.TransportationContractor)
            .ThenInclude(x => x.PriceWeights)
            .Include(x => x.TransportationContractor)
            .ThenInclude(x => x.TransportationContractorInsurances)
            .Include(x => x.TransportationContractor)
            .Include(x => x.TransportationCargo)
            .Include(x => x.TransportationRequest)
            .Include(x => x.PackingPallet)
            .Include(x => x.PackingDestinationAddress)
            .Include(x => x.PackingSourceAddress)
            .Include(x => x.ShippingCost)
            .Include(x => x.TransportationRequestWarehouses)
                .ThenInclude(x => x.PackingProduct)
                    .ThenInclude(x => x.DestinationPackingAddress)
            .Include(x => x.TransportationRequestWarehouses)
                .ThenInclude(x => x.PackingProduct)
                    .ThenInclude(x => x.SourcePackingAddress)
            .Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
    }

    public async Task<(List<GetsTransportationCargoPalletResponseModel> Data, int RowCount)> GetsTransportationCargo(
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
        CT ct)
    {
        var query = DbSet
        .Where(t =>
            (haveShippingType == null ||
                (haveShippingType == true && t.PackingShippingType != null) ||
                (haveShippingType == false && t.PackingShippingType == null)) &&

            (contractorIds == null || contractorIds.Count == 0 || (t.TransportationContractorId != null &&
            contractorIds.Contains(t.TransportationContractorId.Value))) &&

            (fromDate == null || (t.PostageDate != null && t.PostageDate.Value.Date >= fromDate.Value.Date)) &&

            (toDate == null || (t.PostageDate != null && t.PostageDate.Value.Date <= toDate.Value.Date)) &&

            (deliveryMethods == null || deliveryMethods.Count == 0 ||
            (t.DeliveryMethod != null && deliveryMethods.Contains(t.DeliveryMethod.Value))) &&

            (deliveryTypes == null || deliveryTypes.Count == 0 ||
            (t.DeliveryType != null && deliveryTypes.Contains(t.DeliveryType.Value))) &&

            (packingShippingTypes == null || packingShippingTypes.Count == 0 ||
            (t.PackingShippingType != null && packingShippingTypes.Contains(t.PackingShippingType.Value))) &&

            (ids == null || ids.Count == 0 || ids.Contains(t.Id)) &&

            (thirdPartyIds == null || thirdPartyIds.Count == 0 || (t.TransportationCargo.ThirdPartyId != null &&
            thirdPartyIds.Contains(t.TransportationCargo.ThirdPartyId.Value))) &&

            (transportationRequestStatus == null || transportationRequestStatus.Count == 0 || (t.TransportationRequest != null &&
            transportationRequestStatus.Contains(t.TransportationRequest.TransportationRequestStatus))) &&

            (transportationRequestIds == null || transportationRequestIds.Count == 0 || (t.TransportationRequest != null &&
            transportationRequestIds.Contains(t.TransportationRequest.Id))) &&

            (packingIds == null || packingIds.Count == 0 || packingIds.Contains(t.TransportationCargo.PackingId)) &&

            (cargoIds == null || cargoIds.Count == 0 || cargoIds.Contains(t.TransportationCargo.Id)) &&

            (companyId == null || t.TransportationCargo.Packing.CompanyId == companyId) &&

            (productIds == null || productIds.Count == 0 || t.TransportationRequestWarehouses.Any(z => z.PackingProduct != null && productIds.Contains(z.PackingProduct.ProductId))) &&

            (warehouseIds == null || warehouseIds.Count == 0 || t.TransportationRequestWarehouses.Any(z =>
            z.PackingSourceAddress != null && z.PackingSourceAddress.WarehouseId != null &&
            warehouseIds.Contains(z.PackingSourceAddress.WarehouseId.Value))) &&

            (desWarehouseIds == null || desWarehouseIds.Count == 0 || t.TransportationRequestWarehouses.Any(z =>
            z.PackingDestinationAddress != null && z.PackingDestinationAddress.WarehouseId != null &&
            desWarehouseIds.Contains(z.PackingDestinationAddress.WarehouseId.Value))) &&

            (cityIds == null || cityIds.Count == 0 || t.TransportationRequestWarehouses.Any(z =>
            z.PackingDestinationAddress != null && z.PackingDestinationAddress.CityId != null && cityIds.Contains(z.PackingDestinationAddress.CityId.Value))) &&

            (salesChannelTypes == null || salesChannelTypes.Count == 0 || (t.TransportationCargo.Packing != null && t.TransportationCargo.Packing.SalesChannelType != null &&
            salesChannelTypes.Contains(t.TransportationCargo.Packing.SalesChannelType.Value))) &&

            (creatorId == null || t.CreatorId == creatorId) &&

            (string.IsNullOrEmpty(filterData) ||
             EF.Functions.Like(t.TransportationRequest.RequestNumber.ToString(), filterData.MakeLikePattern()) ||
             EF.Functions.Like(t.TransportationCargo.Packing.RequestNumber.ToString(), filterData.MakeLikePattern())))
        .Select(x => new GetsTransportationCargoPalletResponseModel
        {
            Id = x.Id,
            CargoId = x.TransportationCargo.Id,
            CargoCreated = x.TransportationCargo.Created,
            Created = x.Created,
            CreatorId = x.CreatorId,
            PackingNumber = x.TransportationCargo.Packing != null ? x.TransportationCargo.Packing.RequestNumber : null,
            PackingId = x.TransportationCargo.PackingId,
            Status = x.TransportationCargo.Packing != null ? x.TransportationCargo.Packing.Status : null,
            SalesChannelType = x.TransportationCargo.Packing != null ? x.TransportationCargo.Packing.SalesChannelType : null,
            ThirdPartyId = x.TransportationCargo.ThirdPartyId,
            ThirdPartyFullName = x.TransportationCargo.ThirdParty != null ? x.TransportationCargo.ThirdParty.FirstName + " " + x.TransportationCargo.ThirdParty.LastName : null,
            ThirdParty = x.TransportationCargo.ThirdParty != null ? x.TransportationCargo.ThirdPartyName : null,
            TransportationContractorId = x.TransportationContractorId,
            DeliveryMethod = x.DeliveryMethod,
            DeliveryType = x.DeliveryType,
            PackingShippingType = x.PackingShippingType,
            VehicleName = x.VehicleName,
            Driver = x.Driver,
            DriverPhoneNumber = x.DriverPhoneNumber,
            PostageDate = x.PostageDate,
            NumberPlate = x.NumberPlate,
            TransportationRequestId = x.TransportationRequest != null ? x.TransportationRequestId : null,
            TransportationRequestNumber = x.TransportationRequest != null ? x.TransportationRequest.RequestNumber : null,
            PackingPalletId = x.PackingPalletId,
            PalletNumber = x.PalletNumber,
            Weight = x.PackingPallet != null ? x.PackingPallet.Weight : null,
            Height = x.PackingPallet != null ? x.PackingPallet.Height : null,
            PalletPrice = x.Price,
            PalletTransferPrice = x.TransferPrice,
            PalletWeight = x.Weight,
            PalletQuantity = x.Quantity,
            PalletPermitStatus = x.PackingPallet != null ? x.PackingPallet.PalletPermitStatus : null,
            PermitDate = x.PackingPallet != null ? x.PackingPallet.PermitDate : null,
            PermitNumber = x.PackingPallet != null ? x.PackingPallet.PermitNumber : null,
            PackagingSpecType = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.PackagingSpecType : null,
            PackagingSpecTitle = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Title : null,
            PackagingSpecQuantity = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Quantity : 0,
            PackagingSpecLength = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Length : null,
            PackagingSpecWidth = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Width : null,
            PackagingSpecHeight = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Height : null,
            PackagingSpecWeight = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Weight : null,
            PackagingSpecRatio = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Ratio : null,
            PackagingSpecMeasureUnitId = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null && x.PackingPallet.PackagingSpec.MeasureUnitId != null
                ? x.PackingPallet.PackagingSpec.MeasureUnitId : null,
            PackingSourceAddressId = x.PackingSourceAddress != null ? x.PackingSourceAddress.Id : null,
            PackingSourceAddressCityId = x.PackingSourceAddress != null && x.PackingSourceAddress.CityId != null ? x.PackingSourceAddress.CityId : null,
            PackingSourceAddressWarehouseId = x.PackingSourceAddress != null && x.PackingSourceAddress.Warehouse != null ? x.PackingSourceAddress.Warehouse.Id : null,
            PackingSourceAddressWarehouse = x.PackingSourceAddress != null && x.PackingSourceAddress.Warehouse != null ? x.PackingSourceAddress.Warehouse.Name : null,
            PackingSourceAddress = x.PackingSourceAddress != null ? x.PackingSourceAddress.Address : null,
            PackingDestinationAddressId = x.PackingDestinationAddress != null ? x.PackingDestinationAddress.Id : null,
            PackingDestinationAddressCityId = x.PackingDestinationAddress != null && x.PackingDestinationAddress.CityId != null ? x.PackingDestinationAddress.CityId : null,
            PackingDestinationAddressWarehouseId = x.PackingDestinationAddress != null && x.PackingDestinationAddress.Warehouse != null ? x.PackingDestinationAddress.Warehouse.Id : null,
            PackingDestinationAddressWarehouse = x.PackingDestinationAddress != null && x.PackingDestinationAddress.Warehouse != null ? x.PackingDestinationAddress.Warehouse.Name : null,
            PackingDestinationAddress = x.PackingDestinationAddress != null ? x.PackingDestinationAddress.Address : null,
            PackingDestinationPhoneNumber = x.PackingDestinationAddress != null ? x.PackingDestinationAddress.PhoneNumber : null,
            PackingDestinationPostalCode = x.PackingDestinationAddress != null ? x.PackingDestinationAddress.PostalCode : null,
            ShippingCostId = x.ShippingCost != null ? x.ShippingCost.Id : null,
            ShippingCostPrice = x.ShippingCost != null ? x.ShippingCost.Price : null,
            DeliveryDate = x.TransportationCargo.Packing != null ? x.TransportationCargo.Packing.DeliveryDate : null,
            SecurityConfirm = x.PackingPallet.PalletPermitStatus != PalletPermitStatus.PermitConfirmed ?
            true : false,
            SecurityConfirmDate = x.PackingPallet.PalletPermitStatus != PalletPermitStatus.PermitConfirmed ?
            x.PackingPallet.PermitDate : null,
            CargoSecurityConfirm = x.TransportationCargo.SecurityConfirm,
            CargoSecurityConfirmDate = x.TransportationCargo.SecurityConfirmDate,
            CargoDocuments = x.TransportationCargo.TransportationCargoDocuments.Select(x =>
            new CargoDcoumentsDto
            {
                Id = x.Id,
                Url = x.Url,
            }).ToList(),
            PalletProducts = x.TransportationRequestWarehouses != null && x.TransportationRequestWarehouses.Any()
                ? x.TransportationRequestWarehouses.Where(w => !w.IsDeleted && w.Quantity > 0).Select(w => new TransportationRequestWarehousePalletDto
                {
                    Id = w.Id,
                    TransportationCargoPalletId = w.TransportationCargoPalletId,
                    PackingProductId = w.PackingProductId,
                    ProductId = w.PackingProduct != null ? w.PackingProduct.ProductId : null,
                    ProductName = w.PackingProduct != null ? w.PackingProduct.Product.Name : null,
                    ProductCode = w.PackingProduct != null ? w.PackingProduct.Product.Code : null,
                    PalletNumber = w.PalletNumber,
                    PackingSourceAddressId = w.PackingSourceAddress != null ? w.PackingSourceAddress.Id : null,
                    PackingSourceAddressCityId = w.PackingSourceAddress != null && w.PackingSourceAddress.CityId != null ? w.PackingSourceAddress.CityId : null,
                    PackingSourceAddressWarehouseId = w.PackingSourceAddress != null && w.PackingSourceAddress.Warehouse != null ? w.PackingSourceAddress.Warehouse.Id : null,
                    PackingSourceAddressWarehouse = w.PackingSourceAddress != null && w.PackingSourceAddress.Warehouse != null ? w.PackingSourceAddress.Warehouse.Name : null,
                    PackingSourceAddress = w.PackingSourceAddress != null ? w.PackingSourceAddress.Address : null,
                    PackingDestinationAddressId = w.PackingDestinationAddress != null ? w.PackingDestinationAddress.Id : null,
                    PackingDestinationAddressCityId = w.PackingDestinationAddress != null && w.PackingDestinationAddress.CityId != null ? w.PackingDestinationAddress.CityId : null,
                    PackingDestinationAddressWarehouseId = w.PackingDestinationAddress != null && w.PackingDestinationAddress.Warehouse != null ? w.PackingDestinationAddress.Warehouse.Id : null,
                    PackingDestinationAddressWarehouse = w.PackingDestinationAddress != null && w.PackingDestinationAddress.Warehouse != null ? w.PackingDestinationAddress.Warehouse.Name : null,
                    PackingDestinationAddress = w.PackingDestinationAddress != null ? w.PackingDestinationAddress.Address : null,
                    PackingDestinationPhoneNumber = w.PackingDestinationAddress != null ? w.PackingDestinationAddress.PhoneNumber : null,
                    PackingDestinationPostalCode = w.PackingDestinationAddress != null ? w.PackingDestinationAddress.PostalCode : null,
                    Price = w.Price,
                    Quantity = w.Quantity,
                    Created = w.Created,
                    PalletPermitStatus = x.PackingPallet != null ? x.PackingPallet.PalletPermitStatus : null,
                    PermitDate = x.PackingPallet != null ? x.PackingPallet.PermitDate : null,
                    PermitNumber = x.PackingPallet != null ? x.PackingPallet.PermitNumber : null,
                }).ToList()
                : new List<TransportationRequestWarehousePalletDto>()
        });

        query = query.OrderByDescending(t => t.Created).ThenBy(x => x.PostageDate);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsTransportationCargoPalletResponseModel> Data, int RowCount)> GetsTransportationCargoWithoutContractor(
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
        CT ct)
    {
        var query = DbSet
        .Where(t =>

            (t.TransportationContractorId == null) &&

            (haveShippingType == null ||
                (haveShippingType == true && t.PackingShippingType != null) ||
                (haveShippingType == false && t.PackingShippingType == null)) &&

           (fromDate == null || (t.PostageDate != null && t.PostageDate.Value.Date >= fromDate.Value.Date)) &&

            (toDate == null || (t.PostageDate != null && t.PostageDate.Value.Date <= toDate.Value.Date)) &&

            (deliveryMethods == null || deliveryMethods.Count == 0 ||
            (t.DeliveryMethod != null && deliveryMethods.Contains(t.DeliveryMethod.Value))) &&

            (deliveryTypes == null || deliveryTypes.Count == 0 ||
            (t.DeliveryType != null && deliveryTypes.Contains(t.DeliveryType.Value))) &&

            (packingShippingTypes == null || packingShippingTypes.Count == 0 ||
            (t.PackingShippingType != null && packingShippingTypes.Contains(t.PackingShippingType.Value))) &&

            (ids == null || ids.Count == 0 || ids.Contains(t.Id)) &&

            (thirdPartyIds == null || thirdPartyIds.Count == 0 || (t.TransportationCargo.ThirdPartyId != null &&
            thirdPartyIds.Contains(t.TransportationCargo.ThirdPartyId.Value))) &&

            (transportationRequestStatus == null || transportationRequestStatus.Count == 0 || (t.TransportationRequest != null &&
            transportationRequestStatus.Contains(t.TransportationRequest.TransportationRequestStatus))) &&

            (transportationRequestIds == null || transportationRequestIds.Count == 0 || (t.TransportationRequest != null &&
            transportationRequestIds.Contains(t.TransportationRequest.Id))) &&

            (packingIds == null || packingIds.Count == 0 || packingIds.Contains(t.TransportationCargo.PackingId)) &&

            (cargoIds == null || cargoIds.Count == 0 || cargoIds.Contains(t.TransportationCargo.Id)) &&

            (companyId == null || t.TransportationCargo.Packing.CompanyId == companyId) &&

            (productIds == null || productIds.Count == 0 || t.TransportationRequestWarehouses.Any(z =>
            z.PackingProduct != null && productIds.Contains(z.PackingProduct.ProductId))) &&

            (warehouseIds == null || warehouseIds.Count == 0 || t.TransportationRequestWarehouses.Any(z =>
            z.PackingSourceAddress != null && z.PackingSourceAddress.WarehouseId != null &&
            warehouseIds.Contains(z.PackingSourceAddress.WarehouseId.Value))) &&

            (desWarehouseIds == null || desWarehouseIds.Count == 0 || t.TransportationRequestWarehouses.Any(z =>
            z.PackingDestinationAddress != null && z.PackingDestinationAddress.WarehouseId != null &&
            desWarehouseIds.Contains(z.PackingDestinationAddress.WarehouseId.Value))) &&

            (cityIds == null || cityIds.Count == 0 || t.TransportationRequestWarehouses.Any(z =>
            z.PackingDestinationAddress != null && z.PackingDestinationAddress.CityId != null && cityIds.Contains(z.PackingDestinationAddress.CityId.Value))) &&

            (salesChannelTypes == null || salesChannelTypes.Count == 0 || (t.TransportationCargo.Packing != null && t.TransportationCargo.Packing.SalesChannelType != null &&
            salesChannelTypes.Contains(t.TransportationCargo.Packing.SalesChannelType.Value))) &&

            (creatorId == null || t.CreatorId == creatorId) &&

            (string.IsNullOrEmpty(filterData) ||
             EF.Functions.Like(t.TransportationRequest.RequestNumber.ToString(), filterData.MakeLikePattern()) ||
             EF.Functions.Like(t.TransportationCargo.Packing.RequestNumber.ToString(), filterData.MakeLikePattern())))
        .Select(x => new GetsTransportationCargoPalletResponseModel
        {
            Id = x.Id,
            CargoId = x.TransportationCargo.Id,
            CargoCreated = x.TransportationCargo.Created,
            Created = x.Created,
            CreatorId = x.CreatorId,
            CargoSecurityConfirm = x.TransportationCargo.SecurityConfirm,
            CargoSecurityConfirmDate = x.TransportationCargo.SecurityConfirmDate,
            CargoDocuments = x.TransportationCargo.TransportationCargoDocuments.Select(x =>
            new CargoDcoumentsDto
            {
                Id = x.Id,
                Url = x.Url,
            }).ToList(),
            PackingNumber = x.TransportationCargo.Packing != null ? x.TransportationCargo.Packing.RequestNumber : null,
            PackingId = x.TransportationCargo.PackingId,
            Status = x.TransportationCargo.Packing != null ? x.TransportationCargo.Packing.Status : null,
            SalesChannelType = x.TransportationCargo.Packing != null ? x.TransportationCargo.Packing.SalesChannelType : null,
            ThirdPartyId = x.TransportationCargo.ThirdPartyId,
            ThirdPartyFullName = x.TransportationCargo.ThirdParty != null ? x.TransportationCargo.ThirdParty.FirstName + " " + x.TransportationCargo.ThirdParty.LastName : null,
            ThirdParty = x.TransportationCargo.ThirdParty != null ? x.TransportationCargo.ThirdPartyName : null,
            TransportationContractorId = x.TransportationContractorId,
            DeliveryMethod = x.DeliveryMethod,
            DeliveryType = x.DeliveryType,
            PackingShippingType = x.PackingShippingType,
            VehicleName = x.VehicleName,
            Driver = x.Driver,
            DriverPhoneNumber = x.DriverPhoneNumber,
            PostageDate = x.PostageDate,
            NumberPlate = x.NumberPlate,
            TransportationRequestId = x.TransportationRequest != null ? x.TransportationRequestId : null,
            TransportationRequestNumber = x.TransportationRequest != null ? x.TransportationRequest.RequestNumber : null,
            PackingPalletId = x.PackingPalletId,
            PalletNumber = x.PalletNumber,
            Weight = x.PackingPallet != null ? x.PackingPallet.Weight : null,
            Height = x.PackingPallet != null ? x.PackingPallet.Height : null,
            PalletPrice = x.Price,
            PalletTransferPrice = x.TransferPrice,
            PalletWeight = x.Weight,
            PalletQuantity = x.Quantity,
            PalletPermitStatus = x.PackingPallet != null ? x.PackingPallet.PalletPermitStatus : null,
            PermitDate = x.PackingPallet != null ? x.PackingPallet.PermitDate : null,
            PermitNumber = x.PackingPallet != null ? x.PackingPallet.PermitNumber : null,
            PackagingSpecType = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.PackagingSpecType : null,
            PackagingSpecTitle = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Title : null,
            PackagingSpecQuantity = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Quantity : 0,
            PackagingSpecLength = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Length : null,
            PackagingSpecWidth = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Width : null,
            PackagingSpecHeight = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Height : null,
            PackagingSpecWeight = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Weight : null,
            PackagingSpecRatio = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null ? x.PackingPallet.PackagingSpec.Ratio : null,
            PackagingSpecMeasureUnitId = x.PackingPallet != null && x.PackingPallet.PackagingSpec != null && x.PackingPallet.PackagingSpec.MeasureUnitId != null
                ? x.PackingPallet.PackagingSpec.MeasureUnitId : null,
            PackingSourceAddressId = x.PackingSourceAddress != null ? x.PackingSourceAddress.Id : null,
            PackingSourceAddressCityId = x.PackingSourceAddress != null && x.PackingSourceAddress.CityId != null ? x.PackingSourceAddress.CityId : null,
            PackingSourceAddressWarehouseId = x.PackingSourceAddress != null && x.PackingSourceAddress.Warehouse != null ? x.PackingSourceAddress.Warehouse.Id : null,
            PackingSourceAddressWarehouse = x.PackingSourceAddress != null && x.PackingSourceAddress.Warehouse != null ? x.PackingSourceAddress.Warehouse.Name : null,
            PackingSourceAddress = x.PackingSourceAddress != null ? x.PackingSourceAddress.Address : null,
            PackingDestinationAddressId = x.PackingDestinationAddress != null ? x.PackingDestinationAddress.Id : null,
            PackingDestinationAddressCityId = x.PackingDestinationAddress != null && x.PackingDestinationAddress.CityId != null ? x.PackingDestinationAddress.CityId : null,
            PackingDestinationAddressWarehouseId = x.PackingDestinationAddress != null && x.PackingDestinationAddress.Warehouse != null ? x.PackingDestinationAddress.Warehouse.Id : null,
            PackingDestinationAddressWarehouse = x.PackingDestinationAddress != null && x.PackingDestinationAddress.Warehouse != null ? x.PackingDestinationAddress.Warehouse.Name : null,
            PackingDestinationAddress = x.PackingDestinationAddress != null ? x.PackingDestinationAddress.Address : null,
            PackingDestinationPhoneNumber = x.PackingDestinationAddress != null ? x.PackingDestinationAddress.PhoneNumber : null,
            PackingDestinationPostalCode = x.PackingDestinationAddress != null ? x.PackingDestinationAddress.PostalCode : null,
            ShippingCostId = x.ShippingCost != null ? x.ShippingCost.Id : null,
            ShippingCostPrice = x.ShippingCost != null ? x.ShippingCost.Price : null,
            DeliveryDate = x.TransportationCargo.Packing != null ? x.TransportationCargo.Packing.DeliveryDate : null,

            SecurityConfirm = x.PackingPallet.PalletPermitStatus == PalletPermitStatus.PermitConfirmed ?
                true : false,

            SecurityConfirmDate = x.PackingPallet.PalletPermitStatus == PalletPermitStatus.PermitConfirmed ?
                x.PackingPallet.PermitDate : null,

            PalletProducts = x.TransportationRequestWarehouses != null && x.TransportationRequestWarehouses.Any()
                ? x.TransportationRequestWarehouses.Where(w => !w.IsDeleted && w.Quantity > 0).Select(w => new TransportationRequestWarehousePalletDto
                {
                    Id = w.Id,
                    TransportationCargoPalletId = w.TransportationCargoPalletId,
                    PackingProductId = w.PackingProductId,
                    ProductId = w.PackingProduct != null ? w.PackingProduct.ProductId : null,
                    ProductName = w.PackingProduct != null ? w.PackingProduct.Product.Name : null,
                    ProductCode = w.PackingProduct != null ? w.PackingProduct.Product.Code : null,
                    PalletNumber = w.PalletNumber,
                    PackingSourceAddressId = w.PackingSourceAddress != null ? w.PackingSourceAddress.Id : null,
                    PackingSourceAddressCityId = w.PackingSourceAddress != null && w.PackingSourceAddress.CityId != null ? w.PackingSourceAddress.CityId : null,
                    PackingSourceAddressWarehouseId = w.PackingSourceAddress != null && w.PackingSourceAddress.Warehouse != null ? w.PackingSourceAddress.Warehouse.Id : null,
                    PackingSourceAddressWarehouse = w.PackingSourceAddress != null && w.PackingSourceAddress.Warehouse != null ? w.PackingSourceAddress.Warehouse.Name : null,
                    PackingSourceAddress = w.PackingSourceAddress != null ? w.PackingSourceAddress.Address : null,
                    PackingDestinationAddressId = w.PackingDestinationAddress != null ? w.PackingDestinationAddress.Id : null,
                    PackingDestinationAddressCityId = w.PackingDestinationAddress != null && w.PackingDestinationAddress.CityId != null ? w.PackingDestinationAddress.CityId : null,
                    PackingDestinationAddressWarehouseId = w.PackingDestinationAddress != null && w.PackingDestinationAddress.Warehouse != null ? w.PackingDestinationAddress.Warehouse.Id : null,
                    PackingDestinationAddressWarehouse = w.PackingDestinationAddress != null && w.PackingDestinationAddress.Warehouse != null ? w.PackingDestinationAddress.Warehouse.Name : null,
                    PackingDestinationAddress = w.PackingDestinationAddress != null ? w.PackingDestinationAddress.Address : null,
                    PackingDestinationPhoneNumber = w.PackingDestinationAddress != null ? w.PackingDestinationAddress.PhoneNumber : null,
                    PackingDestinationPostalCode = w.PackingDestinationAddress != null ? w.PackingDestinationAddress.PostalCode : null,
                    Price = w.Price,
                    Quantity = w.Quantity,
                    Created = w.Created,
                    PalletPermitStatus = x.PackingPallet != null ? x.PackingPallet.PalletPermitStatus : null,
                    PermitDate = x.PackingPallet != null ? x.PackingPallet.PermitDate : null,
                    PermitNumber = x.PackingPallet != null ? x.PackingPallet.PermitNumber : null,
                }).ToList()
                : new List<TransportationRequestWarehousePalletDto>()
        });

        query = query.OrderByDescending(t => t.Created).ThenBy(x => x.PostageDate);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<TransportationCargoPallet>?> GetTransportationPallets(
        List<long>? ids,
        CT ct)
    {
        return await DbSet
            .IgnoreQueryFilters()
            .Include(x => x.TransportationCargo)
            .Include(x => x.TransportationRequest)
                .ThenInclude(x => x.TransportationCargoPallets)
            .Where(z =>
            (ids == null || ids.Count == 0 || ids.Contains(z.TransportationCargo.Id)))
            .ToListAsync(ct);
    }

    public async Task<List<TransportationCargoPallet>?> GetTransportationPalletsByPackingIds(
        List<long>? ids,
        CT ct)
    {
        return await DbSet
            .IgnoreQueryFilters()
            .Include(x => x.TransportationCargo)
            .Include(x => x.TransportationRequest)
                .ThenInclude(x => x.TransportationCargoPallets)
            .Where(z => ids == null || ids.Contains(z.TransportationCargo.PackingId))
            .ToListAsync(ct);
    }

    public async Task<GetPackingLogesticDetailResponse?> GetLogesticByPackingId(long id, CT ct)
    {
        var query = DbSet
            .Where(t =>
                t.IsDeleted == false &&
                t.TransportationCargo.IsDeleted == false &&
                t.TransportationCargo.PackingId == id)
            .Select(x => new GetPackingLogesticDetailResponse()
            {
                Id = x.Id,
                TransportationContractorId = x.TransportationContractor.Id,
                ThirdPartyId = x.TransportationContractor.ThirdPartyId,
                DeliveryMethod = x.DeliveryMethod,
                DeliveryType = x.DeliveryType,
                PostageDate = x.PostageDate ?? DateTime.Now.Date,
                ThirdParty = $"{x.TransportationContractor.ThirdParty.FirstName} {x.TransportationContractor.ThirdParty.LastName}",
                TransportationContractor = x.TransportationContractor.ThirdParty.Legal.CompanyName,
                PackingShippingType = x.PackingShippingType,
                Driver = x.Driver,
                DriverPhoneNumber = x.DriverPhoneNumber,
                NumberPlate = x.NumberPlate,
                VehicleName = x.VehicleName,
                CargoId = x.TransportationCargoId,
            });

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }
}