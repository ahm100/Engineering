using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyProductById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProductByIdWithScale;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProduct;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
public partial class RequestGoodsSupplyProductRepository : BaseRepository<EngineeringDBContext, RequestGoodsSupplyProduct>, IRequestGoodsSupplyProductRepository
{
    public RequestGoodsSupplyProductRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<RequestGoodsSupplyProduct?> GetRequestGoodsSupplyProductById(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestGoodsSupply.ProjectOperation)
               .ThenInclude(oo => oo.OperationInfo)
                    .ThenInclude(oo => oo.OperationInfoSeasons)
                        .ThenInclude(oo => oo.Season.Branch.Category)

            .Include(oo => oo.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter.CostCenterWarehouses)
            .Include(oo => oo.RequestGoodsSupply)
               .ThenInclude(oo => oo.RequestGoodsSupplyProducts)

            .Include(oo => oo.RequestGoodsSupply)
                .ThenInclude(oo => oo.Project)
                    .ThenInclude(oo => oo.ProjectCostCenters)
                        .ThenInclude(oo => oo.CostCenter)
                            .ThenInclude(oo => oo.CostCenterWarehouses)

            .Include(oo => oo.RequestGoodsSupplyDetails)
               .ThenInclude(oo => oo.RequestGoodsSupplyDetailDocuments)

            .Include(oo => oo.RequestGoodsSupplyDetails)
               .ThenInclude(oo => oo.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation)

            .Include(oo => oo.RequestGoodsSupplyDetails)
                .ThenInclude(oo => oo.ProjectProduct)

            .Include(oo => oo.RequestGoodsSupplyManagements)

            .Where(c =>
                c.Id.Equals(id));

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<RequestGoodsSupplyProduct?> GetSupplyProductByCommercialCommercialRequestId(long commercialRequestId, CT ct)
    {
        var query = DbSet.Where(c => c.Id == commercialRequestId);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }


    public async Task<List<RequestGoodsSupplyProduct>?> GetsRequestGoodsSupplyProductByContractorId(
        DateTime startDate,
        DateTime endDate,
        GoodsSupplyType? type,
        long contractorId,
        long projectId,
        CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestGoodsSupply)
                .ThenInclude(oo => oo.RequestGoodsSupplyProducts)
            .Include(oo => oo.RequestGoodsSupplyDetails)
                .ThenInclude(oo => oo.RequestGoodsSupplyDetailDocuments)
            .Include(oo => oo.RequestGoodsSupplyDetails)
                .ThenInclude(oo => oo.ConsumableVolumeProduct)
                    .ThenInclude(oo => oo.ProjectOperationDetail.ProjectOperation.OperationInfo)
            .Include(oo => oo.RequestGoodsSupply.ProjectOperation)

            .Where(oo =>
                oo.Created.Date >= startDate.Date &&
                oo.RequestGoodsSupplyDetails.Any(x => x.ConsumableVolumeProduct.ProjectOperationDetail.ProjectOperation.Project.Id == projectId) &&
                oo.RequestGoodsSupplyDetails.Any(x => x.ContractorId == contractorId) &&
                oo.Status == GoodsSupplyDetailStatus.CommercialInvoiceConfirmation &&
                oo.Created.Date <= endDate.Date &&
                (type == null || oo.RequestGoodsSupply.Type.Equals(type)))

            .OrderByDescending(oo => oo.Created);

        return await query.ToListAsync(ct);
    }

    public async Task<List<long>?> GetRGPForManagerProductIds(
        CT ct)
    {
        return await DbSet
            .Where(oo =>
                oo.Status == GoodsSupplyDetailStatus.GoodsManagerPending)
            .Select(x => x.ProductGroupId).Distinct().ToListAsync(ct);
    }

    public async Task<RequestGoodsSupplyProduct?> GetRequestGoodsSupplyProductForChangeStatus(
        long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.RequestGoodsSupplyManagements)
            .Include(oo => oo.RequestGoodsSupplyDetails)

                .Where(c => c.Id.Equals(id));

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<GetRequestGoodsSupplyProductByIdResponse?> GetRequestGoodsSupplyProductByIdModeled(long id, CT ct)
    {
        var query = DbSet
           .Include(oo => oo.RequestGoodsSupply.ProjectOperation.OperationInfo)
           .Include(oo => oo.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
           .Include(oo => oo.RequestGoodsSupplyDetails)
           .ThenInclude(oo => oo.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation)
           .Include(oo => oo.RequestGoodsSupplyDetails)
           .ThenInclude(oo => oo.RequestGoodsSupplyDetailDocuments)
           .Include(oo => oo.RequestGoodsSupplyManagements)
                .ThenInclude(oo => oo.Histories)

           .Where(c =>
                c.Id.Equals(id));

        var item = await query.FirstOrDefaultAsync(ct);
        if (item is not null)
        {

            var entity = new GetRequestGoodsSupplyProductByIdResponse()
            {
                Id = item.Id,
                RequestGoodsSupplyId = item.RequestGoodsSupply.Id,
                IsPettyCash = item.RequestGoodsSupply.IsPettyCash,
                ConsumptionRateAndInventoryUrl = item.RequestGoodsSupply.ConsumptionRateAndInventoryUrl,
                ConsumptionAddress = item.RequestGoodsSupply.ConsumptionAddress,
                PurchaseLocation = item.RequestGoodsSupply.PurchaseLocation,
                SerialNumber = item.SerialNumber,
                RequestNumber = $"{item.SerialNumber}-{item.Id}",

                CostCenterId = item.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any() ?
                item.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId :
                null,
                CostCenterName = item.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any() ?
                item.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName :
                null,

                ProjectId = item.RequestGoodsSupply.ProjectOperation.Project.Id,
                ProjectName = item.RequestGoodsSupply.ProjectOperation.Project.ProjectName,
                ProjectManagerId = item.RequestGoodsSupply.ProjectOperation.Project.ProjectManager,
                ProjectOperationId = item.RequestGoodsSupply.ProjectOperation.Id,
                ProjectOperationMeasureId = item.RequestGoodsSupply.ProjectOperation.UnitOfMeasurementId,
                ProjectOperationName = item.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoName,
                ProjectOperationCode = item.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoCode,
                Workload = item.RequestGoodsSupply.ProjectOperation.Workload,
                CategoryId = item.RequestGoodsSupply.OperationInfoSeason is null ? null : item.RequestGoodsSupply.OperationInfoSeason.Season.Branch.Category.Id,
                CategoryName = item.RequestGoodsSupply.OperationInfoSeason is null ? null : item.RequestGoodsSupply.OperationInfoSeason.Season.Branch.Category.CategoryName,
                BranchId = item.RequestGoodsSupply.OperationInfoSeason is null ? null : item.RequestGoodsSupply.OperationInfoSeason.Season.Branch.Id,
                BranchName = item.RequestGoodsSupply.OperationInfoSeason is null ? null : item.RequestGoodsSupply.OperationInfoSeason.Season.Branch.BranchName,
                SeasonId = item.RequestGoodsSupply.OperationInfoSeason is null ? null : item.RequestGoodsSupply.OperationInfoSeason.Season.Id,
                SeasonName = item.RequestGoodsSupply.OperationInfoSeason is null ? null : item.RequestGoodsSupply.OperationInfoSeason.Season.SeasonName,
                ProjectOperationDetailIds = item.RequestGoodsSupplyDetails.Select(d => d.ConsumableVolumeProduct.ProjectOperationDetail.Id).ToList(),
                GetsProjectOperationDetailName = item.RequestGoodsSupplyDetails.Select(d => d.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PrivateName).ToList(),
                GetsProjectOperationDetailCode = item.RequestGoodsSupplyDetails.Select(d => d.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PrivateCode).ToList(),
                Importance = item.Importance,
                Status = item.Status,
                Type = item.RequestGoodsSupply.Type,
                ProductId = item.ProductId,
                RequestedCount = item.RequestedCount,
                SupplyCount = item.RequestGoodsSupplyManagements.Where(z => z.Status == GoodsSupplyManagementStatus.CompleteSupply).Sum(z => z.RequestedCount),
                DelivaryDeadLine = item.DelivaryDeadLine,
                RequestedDate = item.RequestGoodsSupply.RequestedDate,
                Created = item.RequestGoodsSupply.Created,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice,
                TaxPercentage = item.TaxPercentage,
                TaxNumber = item.TaxNumber,
                DiscountByPercentage = item.DiscountByPercentage,
                DiscountByNumber = item.DiscountByNumber,
                DiscountedPrice = item.DiscountedPrice,
                PackingPrice = 0,
                TransferPrice = item.TransferPrice,
                OtherPrice = item.RequestGoodsSupply.OtherPrice,
                FinalPrice = item.FinalPrice,
                CheckGroup = item.CheckGroup,
                ContractorId = item.ContractorId,
                SupplyerId = item.RequestGoodsSupply.SupplyerId,
                BuyerId = item.RequestGoodsSupply.BuyerId,
                CurrencyId = item.RequestGoodsSupply.CurrencyId,
                DestinationWarehouseId = item.DestinationWarehouseId,
                CreatorId = item.RequestGoodsSupply.CreatorId,
                PackageId = item.PackageId,
                PackageCount = item.PackageCount,
                PackageUnitPrice = item.PackageUnitPrice,
                CustomerInvoiceNumber = item.CustomerInvoiceNumber,
                Description = item.Description,
                ManagementDescription = item.ManagementDescription,
                LastDescription = item.LastDescription,
                Documents = item.RequestGoodsSupplyDetails.SelectMany(x => x.RequestGoodsSupplyDetailDocuments.Select(d => d.Url)).Distinct().ToList(),
                Managements = item.RequestGoodsSupplyManagements.Select(z => new GetsRequestGoodsSupplyProductManagement()
                {
                    Id = z.Id,
                    Type = z.Type,
                    Status = z.Status,
                    InvoiceId = z.InvoiceId,
                    WarehouseId = z.WarehouseId,
                    DestinationWarehouseId = z.DestinationWarehouseId,
                    ProductId = z.ReferenceId,
                    RequestedCount = z.RequestedCount,
                    ConfirmedRequestCount = z.ConfirmedRequestCount,
                    AlternateId = z.AlternateId,
                    OperatorAppointmentId = z.OperatorAppointmentId,
                    Description = z.Description,
                    LastDescription = z.LastDescription,
                    AssignmentDate = z.Histories.Max(x => x.AssignmentDate),
                }).ToList(),
            };
            return entity;
        }
        else
        {
            return null;
        }
    }

    public async Task<GetGoodsSupplyProductByIdResponse?> GetGoodsSupplyProductById(long id, CT ct)
    {
        var query = DbSet
           .Where(c => c.Id.Equals(id))

            .Select(item => new GetGoodsSupplyProductByIdResponse()
            {
                Id = item.Id,
                RequestGoodsSupplyId = item.RequestGoodsSupply.Id,
                IsPettyCash = item.RequestGoodsSupply.IsPettyCash,
                ConsumptionAddress = item.RequestGoodsSupply.ConsumptionAddress,
                ConsumptionRateAndInventoryUrl = item.RequestGoodsSupply.ConsumptionRateAndInventoryUrl,
                SerialNumber = item.SerialNumber,
                RequestNumber = $"{item.SerialNumber}-{item.Id}",

                CostCenterId = item.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any() ?
                item.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId :
                null,
                CostCenterName = item.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any() ?
                item.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName :
                null,

                ProjectId = item.RequestGoodsSupply.ProjectOperation.Project.Id,
                ProjectName = item.RequestGoodsSupply.ProjectOperation.Project.ProjectName,
                ProjectManagerId = item.RequestGoodsSupply.ProjectOperation.Project.ProjectManager,
                ProjectOperationId = item.RequestGoodsSupply.ProjectOperation.Id,
                ProjectOperationMeasureId = item.RequestGoodsSupply.ProjectOperation.UnitOfMeasurementId,
                ProjectOperationName = item.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoName,
                ProjectOperationCode = item.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoCode,
                Workload = item.RequestGoodsSupply.ProjectOperation.Workload,
                CategoryId = item.RequestGoodsSupply.OperationInfoSeason.Season.Branch.Category.Id,
                CategoryName = item.RequestGoodsSupply.OperationInfoSeason.Season.Branch.Category.CategoryName,
                BranchId = item.RequestGoodsSupply.OperationInfoSeason.Season.Branch.Id,
                BranchName = item.RequestGoodsSupply.OperationInfoSeason.Season.Branch.BranchName,
                SeasonId = item.RequestGoodsSupply.OperationInfoSeason.Season.Id,
                SeasonName = item.RequestGoodsSupply.OperationInfoSeason.Season.SeasonName,
                ProjectOperationDetailIds = item.RequestGoodsSupplyDetails.Select(d => d.ConsumableVolumeProduct.ProjectOperationDetail.Id).ToList(),
                Importance = item.Importance,
                Status = item.Status,
                Type = item.RequestGoodsSupply.Type,
                ProductId = item.ProductId,
                RequestedCount = item.RequestedCount,
                SupplyCount = item.RequestGoodsSupplyManagements.Where(z => z.Status == GoodsSupplyManagementStatus.CompleteSupply).Sum(z => z.RequestedCount),
                DelivaryDeadLine = item.DelivaryDeadLine,
                RequestedDate = item.RequestGoodsSupply.RequestedDate,
                Created = item.RequestGoodsSupply.Created,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice,
                TaxPercentage = item.TaxPercentage,
                TaxNumber = item.TaxNumber,
                DiscountByPercentage = item.DiscountByPercentage,
                DiscountByNumber = item.DiscountByNumber,
                DiscountedPrice = item.DiscountedPrice,
                PackingPrice = 0,
                TransferPrice = item.TransferPrice,
                OtherPrice = item.RequestGoodsSupply.OtherPrice,
                FinalPrice = item.FinalPrice,
                CheckGroup = item.CheckGroup,
                ContractorId = item.ContractorId,
                SupplyerId = item.RequestGoodsSupply.SupplyerId,
                BuyerId = item.RequestGoodsSupply.BuyerId,
                CurrencyId = item.RequestGoodsSupply.CurrencyId,
                DestinationWarehouseId = item.DestinationWarehouseId,
                CreatorId = item.RequestGoodsSupply.CreatorId,
                PackageId = item.PackageId,
                PackageCount = item.PackageCount,
                PackageUnitPrice = item.PackageUnitPrice,
                CustomerInvoiceNumber = item.CustomerInvoiceNumber,
                Description = item.Description,
                ManagementDescription = item.ManagementDescription,
                LastDescription = item.LastDescription,
            });

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<(List<RequestGoodsSupplyProduct> Data, int RowCount)> GetsRequestGoodsSupplyProductByIds(
        List<long>? ids,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestGoodsSupply.ProjectOperation)
               .ThenInclude(oo => oo.OperationInfo)
                    .ThenInclude(oo => oo.OperationInfoSeasons)
                        .ThenInclude(oo => oo.Season.Branch.Category)
            .Include(oo => oo.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter.CostCenterWarehouses)
            .Include(oo => oo.RequestGoodsSupply)
               .ThenInclude(oo => oo.RequestGoodsSupplyProducts)
            .Include(oo => oo.RequestGoodsSupplyDetails)
               .ThenInclude(oo => oo.RequestGoodsSupplyDetailDocuments)
            .Include(oo => oo.RequestGoodsSupplyDetails)
               .ThenInclude(oo => oo.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation)
            .Include(oo => oo.RequestGoodsSupplyManagements)

            .Where(x => !x.IsDeleted && !x.RequestGoodsSupply.IsDeleted &&
                (ids == null || ids.Contains(x.Id)));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsGoodsSupplyProductModel> Data, int RowCount)> GetsGoodsSupplyProduct(
        List<long>? ids,
        List<long>? requestGoodsSupplyIds,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? productIds,
        List<long>? creatorIds,
        List<long>? warehouseIds,
        List<long>? contractorIds,
        List<long>? buyerIds,
        List<long>? supplyerIds,
        List<long>? managerSelectedProductIds,
        long? thirdPartyId,
        long? cityId,
        long? rGSId,
        long? projectManagerId,
        List<GoodsSupplyDetailImportance>? importances,
        List<GoodsSupplyType>? types,
        List<GoodsSupplyDetailStatus>? statuses,
        List<GoodsSupplyDetailStatus>? removeStatuses,
        DateTime? startDate,
        DateTime? endDate,
        string? requestNumber,
        string? filterDescription,
        string? filterPublicName,
        string? filterOperationInfoName,
        string? filterManagerDescription,
        string? filterData,
        string? customerInvoiceNumber,
        bool isDraft,
        string[]? orderBy,
        bool isExcel,
        long companyId,
        bool checkThirdParty,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Where(x =>
                !x.IsDeleted &&
                !x.RequestGoodsSupply.IsDeleted &&
                x.RequestGoodsSupply.CompanyId == companyId &&
                (ids == null || ids.Contains(x.Id)) &&

                (costCenterIds == null ||
                (!x.RequestGoodsSupply.IsProjectSupply && x.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId)) ||
                (x.RequestGoodsSupply.IsProjectSupply && x.RequestGoodsSupply.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))))) &&

                (cityId == null || x.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenter.CityId == cityId)) &&

                (requestGoodsSupplyIds == null || requestGoodsSupplyIds.Contains(x.RequestGoodsSupplyId)) &&
                (projectIds == null ||
                (!x.RequestGoodsSupply.IsProjectSupply && projectIds.Contains(x.RequestGoodsSupply.ProjectOperation.Project.Id)) ||
                (x.RequestGoodsSupply.IsProjectSupply && projectIds.Contains(x.RequestGoodsSupply.Project.Id))) &&

                (projectManagerId == null ||
                (
                    x.RequestGoodsSupply.ProjectOperation == null
                    ? x.RequestGoodsSupply.Project.ProjectManager.Equals(projectManagerId)
                    : x.RequestGoodsSupply.ProjectOperation.Project.ProjectManager.Equals(projectManagerId)
                )) &&
                (projectOperationIds == null || projectOperationIds.Contains(x.RequestGoodsSupply.ProjectOperation.Id)) &&
                (projectOperationDetailIds == null || x.RequestGoodsSupplyDetails.Any(d => projectOperationDetailIds.Contains(d.ConsumableVolumeProduct.ProjectOperationDetail.Id))) &&
                (productIds == null || productIds.Count == 0 || productIds.Contains(x.ProductId)) &&
                (managerSelectedProductIds == null || managerSelectedProductIds.Count == 0 || managerSelectedProductIds.Contains(x.ProductId)) &&
                (creatorIds == null || creatorIds.Contains(x.RequestGoodsSupply.CreatorId)) &&
                (contractorIds == null || (x.ContractorId.HasValue && contractorIds.Contains(x.ContractorId.Value))) &&
                (buyerIds == null || (x.RequestGoodsSupply.BuyerId.HasValue && buyerIds.Contains(x.RequestGoodsSupply.BuyerId.Value))) &&
                (supplyerIds == null || (x.RequestGoodsSupply.SupplyerId.HasValue && supplyerIds.Contains(x.RequestGoodsSupply.SupplyerId.Value))) &&
                (warehouseIds == null || x.RequestGoodsSupply.Type == GoodsSupplyType.Project && warehouseIds.Contains(x.DestinationWarehouseId!.Value) ||
                 x.RequestGoodsSupplyManagements.Any(m => m.WarehouseId.HasValue && warehouseIds.Contains(m.WarehouseId.Value)) ||
                 x.RequestGoodsSupplyManagements.Any(m => m.DestinationWarehouseId.HasValue && warehouseIds.Contains(m.DestinationWarehouseId.Value))) &&
                (startDate == null || x.RequestGoodsSupply.Created >= startDate) &&
                (endDate == null || x.RequestGoodsSupply.Created.Date <= endDate.Value.Date) &&
                (types == null || types.Contains(x.RequestGoodsSupply.Type)) &&
                (rGSId == null || x.RequestGoodsSupplyId == rGSId) &&
                (statuses == null || statuses.Contains(x.Status)) &&
                (removeStatuses == null || !removeStatuses.Contains(x.Status)) &&
                (customerInvoiceNumber == null || x.CustomerInvoiceNumber == customerInvoiceNumber) &&
                (isDraft == true ? x.Status == GoodsSupplyDetailStatus.Draft : x.Status != GoodsSupplyDetailStatus.Draft) &&
                (string.IsNullOrWhiteSpace(filterManagerDescription) || EF.Functions.Like(x.ManagementDescription, filterManagerDescription.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterOperationInfoName) || EF.Functions.Like(x.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoName, filterOperationInfoName.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterPublicName) || x.RequestGoodsSupplyDetails.Any(x => EF.Functions.Like(x.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PublicName, filterPublicName.MakeLikePattern()))) &&
                (string.IsNullOrWhiteSpace(filterDescription) || x.RequestGoodsSupplyDetails.Any(x => EF.Functions.Like(x.ConsumableVolumeProduct.ProjectOperationDetail.Description, filterDescription.MakeLikePattern()))) &&
                (string.IsNullOrWhiteSpace(requestNumber) || EF.Functions.Like(x.RequestGoodsSupply.SerialNumber.ToString() + "-" + x.RequestGoodsSupply.Id.ToString(), requestNumber.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                EF.Functions.Like(x.SerialNumber.ToString() + "-" + x.Id.ToString(), filterData.MakeLikePattern()) ||
                EF.Functions.Like(x.ManagementDescription, filterData.MakeLikePattern()) ||
                EF.Functions.Like(x.Description, filterData.MakeLikePattern())) &&
                (checkThirdParty == false ||
                (x.RequestGoodsSupply.IsProjectSupply && (!x.RequestGoodsSupply.Project.ProjectThirdParties.Any() || x.RequestGoodsSupply.Project.ProjectThirdParties.Any(x => x.AuthorizedThirdPartyId == thirdPartyId))) ||
                (!x.RequestGoodsSupply.IsProjectSupply && (!x.RequestGoodsSupply.ProjectOperation.Project.ProjectThirdParties.Any() || x.RequestGoodsSupply.ProjectOperation.Project.ProjectThirdParties.Any(x => x.AuthorizedThirdPartyId == thirdPartyId))))
                )
            .Select(item => new GetsGoodsSupplyProductModel()
            {
                Id = item.Id,
                RequestGoodsSupplyId = item.RequestGoodsSupply.Id,
                IsPettyCash = item.RequestGoodsSupply.IsPettyCash,
                IsProjectSupply = item.RequestGoodsSupply.IsProjectSupply,
                DeliveryDeadline = item.RequestGoodsSupply.DeliveryDeadline,
                RegistrationNumber = item.RequestGoodsSupply.RegistrationNumber,
                RequestingOrganizationId = item.RequestGoodsSupply.RequestingOrganizationId,
                DescriptionEn = item.RequestGoodsSupply.DescriptionEn,
                ConsumptionRateAndInventoryUrl = item.RequestGoodsSupply.ConsumptionRateAndInventoryUrl,
                ConsumptionAddress = item.RequestGoodsSupply.ConsumptionAddress,
                PurchaseLocation = item.RequestGoodsSupply.PurchaseLocation,
                PurchaseReason = item.RequestGoodsSupply.PurchaseReason,
                SerialNumber = item.SerialNumber,
                RGSRequestNumber = item.RequestGoodsSupply.RequestSerialNumber,
                RequestNumber = item.SerialNumber.ToString() + "-" + item.Id.ToString(),
                CostCenterId = item.RequestGoodsSupply.ProjectOperation != null ? item.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id :
                item.RequestGoodsSupply.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,
                CostCenterName = item.RequestGoodsSupply.ProjectOperation != null ? item.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName :
                item.RequestGoodsSupply.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
                DefaultWarehouseId = item.RequestGoodsSupply.ProjectOperation != null ?
                item.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterWarehouses.FirstOrDefault(x => x.IsDefault).WarehouseId :
                item.RequestGoodsSupply.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterWarehouses.FirstOrDefault(x => x.IsDefault).WarehouseId,
                ProjectId = item.RequestGoodsSupply.ProjectOperation != null ? item.RequestGoodsSupply.ProjectOperation.Project.Id :
                item.RequestGoodsSupply.Project.Id,
                ProjectName = item.RequestGoodsSupply.ProjectOperation != null ? item.RequestGoodsSupply.ProjectOperation.Project.ProjectName :
                item.RequestGoodsSupply.Project.ProjectName,
                ProjectManagerId = item.RequestGoodsSupply.ProjectOperation != null ? item.RequestGoodsSupply.ProjectOperation.Project.ProjectManager :
                item.RequestGoodsSupply.Project.ProjectManager,
                ProjectOperationId = item.RequestGoodsSupply.ProjectOperation.Id,
                ProjectOperationMeasureId =
                    item.RequestGoodsSupply.ProjectOperation.UnitOfMeasurementId,
                ProjectOperationName = item.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoName,
                ProjectOperationCode = item.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoCode,
                Workload = item.RequestGoodsSupply.ProjectOperation.Workload,
                CategoryId = item.RequestGoodsSupply.OperationInfoSeason.Season.Branch.Category.Id,
                CategoryName = item.RequestGoodsSupply.OperationInfoSeason.Season.Branch.Category.CategoryName,
                BranchId = item.RequestGoodsSupply.OperationInfoSeason.Season.Branch.Id,
                BranchName = item.RequestGoodsSupply.OperationInfoSeason.Season.Branch.BranchName,
                SeasonId = item.RequestGoodsSupply.OperationInfoSeason.Season.Id,
                SeasonName = item.RequestGoodsSupply.OperationInfoSeason.Season.SeasonName,
                ProjectOperationDetailIds =
                    item.RequestGoodsSupplyDetails
                    .Where(d =>
                    d.ConsumableVolumeProduct != null &&
                    d.ConsumableVolumeProduct.ProjectOperationDetail != null)
                    .Select(d => d.ConsumableVolumeProduct.ProjectOperationDetail.Id)
                    .ToList(),
                Importance = item.RequestGoodsSupplyDetails.FirstOrDefault().Importance,
                Status = item.Status,
                Type = item.RequestGoodsSupply.Type,
                ProductId = item.ProductId,
                RequestedCount = item.RequestedCount,
                CanClose = item.RequestGoodsSupplyDetails
                    .All(x => x.Status == GoodsSupplyDetailStatus.New || x.Status == GoodsSupplyDetailStatus.ProjectManagerReturned || x.Status == GoodsSupplyDetailStatus.ProjectManagerRejected ||
                              x.Status == GoodsSupplyDetailStatus.ManagementReturned || x.Status == GoodsSupplyDetailStatus.ManagementRejected || x.Status == GoodsSupplyDetailStatus.SupplyUnitReturned ||
                              x.Status == GoodsSupplyDetailStatus.PendingForSupply),
                SupplyCount = item.RequestGoodsSupplyManagements.Where(z => z.Status == GoodsSupplyManagementStatus.CompleteSupply).Sum(z => z.RequestedCount),
                DelivaryDeadLine = item.RequestGoodsSupplyDetails.FirstOrDefault().DelivaryDeadLine,
                RequestedDate = item.RequestGoodsSupply.RequestedDate,
                Created = item.RequestGoodsSupply.Created,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice,
                TaxPercentage = item.TaxPercentage,
                TaxNumber = item.TaxNumber,
                DiscountByPercentage = item.DiscountByPercentage,
                DiscountByNumber = item.DiscountByNumber,
                DiscountedPrice = item.DiscountedPrice,
                PackingPrice = item.PackingPrice,
                TransferPrice = item.TransferPrice,
                SupplyOtherPrice = item.RequestGoodsSupply.OtherPrice,
                SupplyProductCount = item.RequestGoodsSupply.RequestGoodsSupplyProducts.Count,
                ProductFinalPrice = item.FinalPrice,
                CheckGroup = item.CheckGroup,
                ContractorId = item.RequestGoodsSupplyDetails.FirstOrDefault().ContractorId,
                SupplyerId = item.RequestGoodsSupply.SupplyerId,
                BuyerId = item.RequestGoodsSupply.BuyerId,
                CurrencyId = item.RequestGoodsSupply.CurrencyId,
                DestinationWarehouseId = item.DestinationWarehouseId,
                CreatorId = item.RequestGoodsSupply.CreatorId,
                PackageId = item.PackageId,
                PackageCount = item.PackageCount,
                PackageUnitPrice = item.PackageUnitPrice,
                CustomerInvoiceNumber = item.CustomerInvoiceNumber,
                Description = item.Description,
                ManagementDescription = item.ManagementDescription,
                LastDescription = item.LastDescription,
                DefaultManagerSet = item.RequestGoodsSupplyDetails.Any(x => x.ProjectProduct != null && x.ProjectProduct.DefaultManagerSet == true),
                OperatorAppointmentIds = item.RequestGoodsSupplyManagements.Where(x => x.OperatorAppointmentId != null).Select(x => x.OperatorAppointmentId!.Value).ToList(),
                HasBetweenStock = item.RequestGoodsSupplyManagements == null ? false : item.RequestGoodsSupplyManagements.Any(x => x.Type != GoodsSupplyManagementType.Commerce) ? true : false,
                CanHaveWarehouse = GSDSRules.CanHaveWarehouse.Contains(item.Status) ? true : false,
                HaveDocuments = item.RequestGoodsSupplyDetails.Any(x => x.RequestGoodsSupplyDetailDocuments.Any()),
            });

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        if (isExcel == true && (pageIndex == 0 || pageSize == 0))
            query = query.Page(1, 1000);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsRequestGoodsSupplyProductModel> Data, int RowCount)> GetsRequestGoodsSupplyProduct(
        List<long>? ids,
        List<long>? requestGoodsSupplyIds,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? productIds,
        List<long>? creatorIds,
        List<long>? warehouseIds,
        long? cityId,
        long? projectManagerId,
        long? thirdPartyId,
        bool chechThirdParty,
        List<GoodsSupplyDetailImportance>? importances,
        List<GoodsSupplyType>? types,
        List<GoodsSupplyDetailStatus>? statuses,
        List<GoodsSupplyDetailStatus>? removeStatuses,
        DateTime? startDate,
        DateTime? endDate,
        string? requestNumber,
        string? filterDescription,
        string? filterPublicName,
        string? filterOperationInfoName,
        string? filterManagerDescription,
        string? filterData,
        string? customerInvoiceNumber,
        string[]? orderBy,
        bool isExcel,
        bool containDraft,
        int pageIndex,
        int pageSize, CT ct)
    {
        IQueryable<RequestGoodsSupplyProduct> query = DbSet;

        if (ids is not null && ids.Count > 0)
            query = query.Where(x => ids.Contains(x.Id));

        if (requestGoodsSupplyIds is not null && requestGoodsSupplyIds.Count > 0)
            query = query.Where(x => requestGoodsSupplyIds.Contains(x.RequestGoodsSupplyId));

        if (costCenterIds is not null && costCenterIds.Count > 0)
            query = query.Where(x => x.RequestGoodsSupplyDetails.FirstOrDefault().ProjectProduct == null ? x.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId)) :
            x.RequestGoodsSupply.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId)));

        if (projectIds is not null && projectIds.Count > 0)
            query = query.Where(x => x.RequestGoodsSupplyDetails.FirstOrDefault().ProjectProduct == null ? projectIds.Contains(x.RequestGoodsSupply.ProjectOperation.Project.Id) :
            projectIds.Contains(x.RequestGoodsSupply.Project.Id));

        if (projectManagerId is not null)
            query = query.Where(x => x.RequestGoodsSupplyDetails.FirstOrDefault().ProjectProduct == null ? x.RequestGoodsSupply.ProjectOperation.Project.ProjectManager == projectManagerId :
            x.RequestGoodsSupply.Project.ProjectManager == projectManagerId);

        if (cityId is not null)
            query = query.Where(x => x.RequestGoodsSupplyDetails.FirstOrDefault().ProjectProduct == null ? x.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenter.CityId == cityId) :
            x.RequestGoodsSupply.Project.ProjectCostCenters.Any(x => x.CostCenter.CityId == cityId));

        if (projectOperationIds is not null && projectOperationIds.Count > 0)
            query = query.Where(x => projectOperationIds.Contains(x.RequestGoodsSupply.ProjectOperation.Id));

        if (projectOperationDetailIds is not null && projectOperationDetailIds.Count > 0)
            query = query.Where(x => x.RequestGoodsSupplyDetails.Any(d => projectOperationDetailIds.Contains(d.ConsumableVolumeProduct.ProjectOperationDetail.Id)));

        if (productIds is not null && productIds.Count > 0)
            query = query.Where(x => productIds.Contains(x.ProductId));

        if (startDate is not null)
            query = query.Where(x => x.RequestGoodsSupply.Created >= startDate);

        if (endDate is not null)
            query = query.Where(x => x.RequestGoodsSupply.Created.Date <= endDate.Value.Date);

        if (types is not null && types.Count > 0)
            query = query.Where(x => types.Contains(x.RequestGoodsSupply.Type));

        if (statuses is not null && statuses.Count > 0)
            query = query.Where(x => statuses.Contains(x.Status));

        if (removeStatuses is not null && removeStatuses.Count > 0)
            query = query.Where(x => !removeStatuses.Contains(x.Status));

        if (creatorIds is not null && creatorIds.Count > 0)
            query = query.Where(x => creatorIds.Contains(x.RequestGoodsSupply.CreatorId));

        if (customerInvoiceNumber is not null)
            query = query.Where(x => x.CustomerInvoiceNumber == customerInvoiceNumber);

        if (warehouseIds is not null && warehouseIds.Count > 0)
            query = query.Where(x => x.RequestGoodsSupply.Type == GoodsSupplyType.Project && warehouseIds.Contains(x.DestinationWarehouseId!.Value) ||
                 x.RequestGoodsSupplyManagements.Any(m => m.WarehouseId.HasValue && warehouseIds.Contains(m.WarehouseId.Value)) ||
                 x.RequestGoodsSupplyManagements.Any(m => m.DestinationWarehouseId.HasValue && warehouseIds.Contains(m.DestinationWarehouseId.Value)));

        if (!string.IsNullOrWhiteSpace(filterManagerDescription))
            query = query.Where(x => EF.Functions.Like(x.ManagementDescription, filterManagerDescription.MakeLikePattern()));

        if (!string.IsNullOrWhiteSpace(filterOperationInfoName))
            query = query.Where(x => EF.Functions.Like(x.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoName, filterOperationInfoName.MakeLikePattern()));

        if (!string.IsNullOrWhiteSpace(filterPublicName))
            query = query.Where(x => x.RequestGoodsSupplyDetails.Any(x => EF.Functions.Like(x.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PublicName, filterPublicName.MakeLikePattern())));

        if (!string.IsNullOrWhiteSpace(filterDescription))
            query = query.Where(x => x.RequestGoodsSupplyDetails.Any(x => EF.Functions.Like(x.ConsumableVolumeProduct.ProjectOperationDetail.Description, filterDescription.MakeLikePattern())));

        if (!string.IsNullOrWhiteSpace(requestNumber))
            query = query.Where(x => EF.Functions.Like(x.RequestGoodsSupply.SerialNumber.ToString() + "-" + x.RequestGoodsSupply.Id.ToString(), requestNumber.MakeLikePattern()));

        if (!string.IsNullOrWhiteSpace(filterData))
            query = query.Where(x => EF.Functions.Like(x.SerialNumber.ToString() + "-" + x.Id.ToString(), filterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.ManagementDescription, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.Description, filterData.MakeLikePattern()));

        if (chechThirdParty is true)
            query = query.Where(x => (x.RequestGoodsSupply.IsProjectSupply && x.RequestGoodsSupply.Project.ProjectThirdParties.Any(x => x.AuthorizedThirdPartyId == thirdPartyId)) ||
            (!x.RequestGoodsSupply.IsProjectSupply && x.RequestGoodsSupply.ProjectOperation.Project.ProjectThirdParties.Any(x => x.AuthorizedThirdPartyId == thirdPartyId)));

        if (containDraft == false)
            query = query.Where(x => x.RequestGoodsSupply.Status != GoodsSupplyStatus.Draft);

        var count = await query.CountAsync(ct);

        query = query.OrderByDescending(oo => oo.Created);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        if (isExcel == true && (pageIndex == 0 || pageSize == 0))
            query = query.Page(1, 10000);

        var newQuery = query
            .Select(item => new GetsRequestGoodsSupplyProductModel()
            {
                Id = item.Id,
                RequestGoodsSupplyId = item.RequestGoodsSupplyId,
                RGSRequestNumber = item.RequestGoodsSupply.RequestSerialNumber,
                IsPettyCash = item.RequestGoodsSupply.IsPettyCash,
                ConsumptionRateAndInventoryUrl = item.RequestGoodsSupply.ConsumptionRateAndInventoryUrl,
                ConsumptionAddress = item.RequestGoodsSupply.ConsumptionAddress,
                SerialNumber = item.SerialNumber,
                Importance = item.Importance,
                Status = item.Status,
                Type = item.RequestGoodsSupply.Type,
                ProductId = item.ProductId,
                RequestedCount = item.RequestedCount,
                DelivaryDeadLine = item.DelivaryDeadLine,
                RequestedDate = item.RequestGoodsSupply.RequestedDate,
                Created = item.RequestGoodsSupply.Created,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice,
                TaxPercentage = item.TaxPercentage,
                TaxNumber = item.TaxNumber,
                DiscountByPercentage = item.DiscountByPercentage,
                DiscountByNumber = item.DiscountByNumber,
                DiscountedPrice = item.DiscountedPrice,
                PackingPrice = item.PackingPrice,
                TransferPrice = item.TransferPrice,
                OtherPrice = item.RequestGoodsSupply.OtherPrice ?? 0 / item.RequestGoodsSupply.RequestGoodsSupplyProducts.Count,
                FinalPrice = item.FinalPrice + (item.RequestGoodsSupply.OtherPrice ?? 0 / item.RequestGoodsSupply.RequestGoodsSupplyProducts.Count),
                CheckGroup = item.CheckGroup,
                ContractorId = item.ContractorId,
                SupplyerId = item.RequestGoodsSupply.SupplyerId,
                BuyerId = item.RequestGoodsSupply.BuyerId,
                CurrencyId = item.RequestGoodsSupply.CurrencyId,
                DestinationWarehouseId = item.DestinationWarehouseId,
                CreatorId = item.RequestGoodsSupply.CreatorId,
                PackageId = item.PackageId,
                PackageCount = item.PackageCount,
                PackageUnitPrice = item.PackageUnitPrice,
                CustomerInvoiceNumber = item.CustomerInvoiceNumber,
                Description = item.Description,
                ManagementDescription = item.ManagementDescription,
                LastDescription = item.LastDescription,
                PurchaseLocation = item.RequestGoodsSupply.PurchaseLocation,
                PurchaseReason = item.RequestGoodsSupply.PurchaseReason,

                CostCenterId = item.RequestGoodsSupply.ProjectOperationId != null ? item.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id :
                    item.RequestGoodsSupply.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,

                CostCenterName = item.RequestGoodsSupply.ProjectOperationId != null ? item.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName :
                    item.RequestGoodsSupply.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
                ProjectId = item.RequestGoodsSupply.ProjectOperationId != null ? item.RequestGoodsSupply.ProjectOperation.Project.Id :
                    item.RequestGoodsSupply.Project.Id,
                ProjectName = item.RequestGoodsSupply.ProjectOperationId != null ? item.RequestGoodsSupply.ProjectOperation.Project.ProjectName :
                    item.RequestGoodsSupply.Project.ProjectName,
                ProjectManagerId = item.RequestGoodsSupply.ProjectOperationId != null ? item.RequestGoodsSupply.ProjectOperation.Project.ProjectManager :
                    item.RequestGoodsSupply.Project.ProjectManager,
                CategoryId = item.RequestGoodsSupply.OperationInfoSeason != null ? item.RequestGoodsSupply.OperationInfoSeason.Season.Branch.CategoryId :
                    item.RequestGoodsSupply.Project.ProjectCategories.FirstOrDefault().Category.Id,
                CategoryName = item.RequestGoodsSupply.OperationInfoSeason != null ? item.RequestGoodsSupply.OperationInfoSeason.Season.Branch.Category.CategoryName :
                    item.RequestGoodsSupply.Project.ProjectCategories.FirstOrDefault().Category.CategoryName,

                ProjectOperationId = item.RequestGoodsSupply.ProjectOperationId,
                ProjectOperationMeasureId = item.RequestGoodsSupply.ProjectOperation.UnitOfMeasurementId,
                ProjectOperationName = item.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoName,
                ProjectOperationCode = item.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoCode,
                Workload = item.RequestGoodsSupply.ProjectOperation.Workload,
                BranchId = item.RequestGoodsSupply.OperationInfoSeason.Season.BranchId,
                BranchName = item.RequestGoodsSupply.OperationInfoSeason.Season.Branch.BranchName,
                SeasonId = item.RequestGoodsSupply.OperationInfoSeason.Season.Id,
                SeasonName = item.RequestGoodsSupply.OperationInfoSeason.Season.SeasonName,

                ProjectOperationDetailIds = item.RequestGoodsSupplyDetails.Where(d =>
                    d.ConsumableVolumeProductId != null &&
                    d.ConsumableVolumeProduct != null &&
                    d.ConsumableVolumeProduct.ProjectOperationDetail != null
                )
                .Select(d => d.ConsumableVolumeProduct.ProjectOperationDetail.Id)
                .ToList(),

                SupplyCount = item.RequestGoodsSupplyManagements.Where(z => z.Status == GoodsSupplyManagementStatus.CompleteSupply).Sum(z => z.RequestedCount),

                CanClose = item.RequestGoodsSupplyDetails.All(x => x.Status == GoodsSupplyDetailStatus.New || x.Status == GoodsSupplyDetailStatus.ProjectManagerReturned || x.Status == GoodsSupplyDetailStatus.ProjectManagerRejected ||
                          x.Status == GoodsSupplyDetailStatus.ManagementReturned || x.Status == GoodsSupplyDetailStatus.ManagementRejected || x.Status == GoodsSupplyDetailStatus.SupplyUnitReturned ||
                          x.Status == GoodsSupplyDetailStatus.PendingForSupply),

                OperatorAppointmentIds = item.RequestGoodsSupplyManagements.Where(x => x.OperatorAppointmentId != null).Select(x => x.OperatorAppointmentId!.Value).ToList(),
                Managements = item.RequestGoodsSupplyManagements.Select(z => new GetsRequestGoodsSupplyProductManagementModel()
                {
                    Id = z.Id,
                    Type = z.Type,
                    Status = z.Status,
                    InvoiceId = z.InvoiceId,
                    WarehouseId = z.WarehouseId,
                    DestinationWarehouseId = z.DestinationWarehouseId,
                    ProductId = z.ReferenceId,
                    RequestedCount = z.RequestedCount,
                    ConfirmedRequestCount = z.ConfirmedRequestCount,
                    AlternateId = z.AlternateId,
                    OperatorAppointmentId = z.OperatorAppointmentId,
                    Description = z.Description,
                    LastDescription = z.LastDescription,
                    AssignmentDate = z.AssignmentDate,
                }).ToList(),
            });
        var items = await newQuery.ToListAsync(ct);
        return (items, count);
    }

    public async Task<decimal> GetsTotalPriceRequestGoodsSupplyProduct(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? productIds,
        List<long>? creatorIds,
        List<long>? warehouseIds,
        long? cityId,
        long? projectManagerId,
        List<GoodsSupplyDetailImportance>? importances,
        List<GoodsSupplyType>? types,
        List<GoodsSupplyDetailStatus>? statuses,
        List<GoodsSupplyDetailStatus>? removeStatuses,
        DateTime? startDate,
        DateTime? endDate,
        string? requestNumber,
        string? filterDescription,
        string? filterPublicName,
        string? filterOperationInfoName,
        string? filterManagerDescription,
        string? filterData,
        string? customerInvoiceNumber,
        CT ct)
    {
        decimal totalFinalPrice = 0;
        var query = await DbSet

            .Where(x => !x.IsDeleted && !x.RequestGoodsSupply.IsDeleted &&
                (ids == null || ids.Contains(x.Id)) &&
                (costCenterIds == null || x.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (cityId == null || x.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenter.CityId == cityId)) &&
                (projectIds == null || projectIds.Contains(x.RequestGoodsSupply.ProjectOperation.Project.Id)) &&
                (projectManagerId == null || x.RequestGoodsSupply.ProjectOperation.Project.ProjectManager.Equals(projectManagerId)) &&
                (projectOperationIds == null || projectOperationIds.Contains(x.RequestGoodsSupply.ProjectOperation.Id)) &&
                (projectOperationDetailIds == null || x.RequestGoodsSupplyDetails.Any(d => projectOperationDetailIds.Contains(d.ConsumableVolumeProduct.ProjectOperationDetail.Id))) &&
                (productIds == null || productIds.Count == 0 || productIds.Contains(x.ProductId)) &&
                (creatorIds == null || creatorIds.Contains(x.RequestGoodsSupply.CreatorId)) &&
                (warehouseIds == null || x.RequestGoodsSupply.Type == GoodsSupplyType.Project && warehouseIds.Contains(x.DestinationWarehouseId!.Value) ||
                 x.RequestGoodsSupplyManagements.Any(m => m.WarehouseId.HasValue && warehouseIds.Contains(m.WarehouseId.Value)) ||
                 x.RequestGoodsSupplyManagements.Any(m => m.DestinationWarehouseId.HasValue && warehouseIds.Contains(m.DestinationWarehouseId.Value))) &&
                (startDate == null || x.RequestGoodsSupply.Created >= startDate) &&
                (endDate == null || x.RequestGoodsSupply.Created.Date <= endDate.Value.Date) &&
                (types == null || types.Contains(x.RequestGoodsSupply.Type)) &&
                (importances == null || importances.Contains(x.Importance)) &&
                (statuses == null || statuses.Contains(x.Status)) &&
                (removeStatuses == null || !removeStatuses.Contains(x.Status)) &&
                (customerInvoiceNumber == null || x.CustomerInvoiceNumber == customerInvoiceNumber) &&
                (string.IsNullOrWhiteSpace(filterManagerDescription) || EF.Functions.Like(x.ManagementDescription, filterManagerDescription.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterOperationInfoName) || EF.Functions.Like(x.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoName, filterOperationInfoName.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterPublicName) || x.RequestGoodsSupplyDetails.Any(x => EF.Functions.Like(x.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PublicName, filterPublicName.MakeLikePattern()))) &&
                (string.IsNullOrWhiteSpace(filterDescription) || x.RequestGoodsSupplyDetails.Any(x => EF.Functions.Like(x.ConsumableVolumeProduct.ProjectOperationDetail.Description, filterDescription.MakeLikePattern()))) &&
                (string.IsNullOrWhiteSpace(requestNumber) || EF.Functions.Like(x.RequestGoodsSupply.SerialNumber.ToString() + "-" + x.RequestGoodsSupply.Id.ToString(), requestNumber.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                 EF.Functions.Like(x.SerialNumber.ToString() + "-" + x.Id.ToString(), filterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.ManagementDescription, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.Description, filterData.MakeLikePattern()))
                )
            .Select(item => item.FinalPrice + (item.RequestGoodsSupply.OtherPrice ?? 0 / item.RequestGoodsSupply.RequestGoodsSupplyProducts.Count)).Where(x => x != null).Select(x => (decimal)x!).ToListAsync();

        if (query != null && query.Count > 0)
            totalFinalPrice = query.Sum(x => x);

        return totalFinalPrice;
    }


    public async Task<(List<GetRequestGoodsSupplyProductByIdResponse> Data, int RowCount)> GetsRequestGoodsSupplyProduct2(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? productIds,
        List<long>? creatorIds,
        List<long>? warehouseIds,
        long? cityId,
        long? projectManagerId,
        List<GoodsSupplyType>? types,
        List<GoodsSupplyDetailStatus>? statuses,
        List<GoodsSupplyDetailStatus>? removeStatuses,
        DateTime? startDate,
        DateTime? endDate,
        string? requestNumber,
        string? filterDescription,
        string? filterPublicName,
        string? filterOperationInfoName,
        string? filterManagerDescription,
        string? filterData,
        string? customerInvoiceNumber,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
       CT ct)
    {
        var query = BuildQueryGetsRequestGoodsSupplyProduct(
            ids,
            costCenterIds,
            projectIds,
            projectOperationIds,
            projectOperationDetailIds,
            productIds,
            creatorIds,
            warehouseIds,
            cityId,
            projectManagerId,
            types,
            statuses,
            removeStatuses,
            startDate,
            endDate,
            requestNumber,
            filterDescription,
            filterPublicName,
            filterOperationInfoName,
            filterManagerDescription,
            filterData,
            customerInvoiceNumber);

        query = query.OrderByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    private IQueryable<GetRequestGoodsSupplyProductByIdResponse> BuildQueryGetsRequestGoodsSupplyProduct(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? productIds,
        List<long>? creatorIds,
        List<long>? warehouseIds,
        long? cityId,
        long? projectManagerId,
        List<GoodsSupplyType>? types,
        List<GoodsSupplyDetailStatus>? statuses,
        List<GoodsSupplyDetailStatus>? removeStatuses,
        DateTime? startDate,
        DateTime? endDate,
        string? requestNumber,
        string? filterDescription,
        string? filterPublicName,
        string? filterOperationInfoName,
        string? filterManagerDescription,
        string? filterData,
        string? customerInvoiceNumber)
    {
        var query = DbSet
            .Include(x => x.RequestGoodsSupplyDetails)

            .Where(x => !x.RequestGoodsSupply.IsDeleted).IgnoreQueryFilters().AsQueryable();

        var newQuery = query
            .Where(x => !x.IsDeleted &&
                (ids == null || ids.Contains(x.Id)) &&
                (costCenterIds == null || x.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (cityId == null || x.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenter.CityId == cityId)) &&
                (projectIds == null || projectIds.Contains(x.RequestGoodsSupply.ProjectOperation.Project.Id)) &&
                (projectManagerId == null || x.RequestGoodsSupply.ProjectOperation.Project.ProjectManager.Equals(projectManagerId)) &&
                (projectOperationIds == null || projectOperationIds.Contains(x.RequestGoodsSupply.ProjectOperation.Id)) &&
                (projectOperationDetailIds == null || x.RequestGoodsSupplyDetails.Any(d => projectOperationDetailIds.Contains(d.ConsumableVolumeProduct.ProjectOperationDetail.Id))) &&
                (productIds == null || productIds.Count == 0 || productIds.Contains(x.ProductId)) &&
                (creatorIds == null || creatorIds.Contains(x.RequestGoodsSupply.CreatorId)) &&
                (warehouseIds == null || x.RequestGoodsSupply.Type == GoodsSupplyType.Project && warehouseIds.Contains(x.DestinationWarehouseId!.Value) ||
                 x.RequestGoodsSupplyManagements.Any(m => m.WarehouseId.HasValue && warehouseIds.Contains(m.WarehouseId.Value)) ||
                 x.RequestGoodsSupplyManagements.Any(m => m.DestinationWarehouseId.HasValue && warehouseIds.Contains(m.DestinationWarehouseId.Value))) &&
                (startDate == null || x.RequestGoodsSupply.Created >= startDate) &&
                (endDate == null || x.RequestGoodsSupply.Created.Date <= endDate.Value.Date) &&
                (types == null || types.Contains(x.RequestGoodsSupply.Type)) &&
                (statuses == null || statuses.Contains(x.Status)) &&
                (removeStatuses == null || !removeStatuses.Contains(x.Status)) &&
                (customerInvoiceNumber == null || x.CustomerInvoiceNumber == customerInvoiceNumber) &&
                (string.IsNullOrWhiteSpace(filterManagerDescription) || EF.Functions.Like(x.ManagementDescription, filterManagerDescription.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterOperationInfoName) || EF.Functions.Like(x.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoName, filterOperationInfoName.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterPublicName) || x.RequestGoodsSupplyDetails.Any(x => EF.Functions.Like(x.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PublicName, filterPublicName.MakeLikePattern()))) &&
                (string.IsNullOrWhiteSpace(filterDescription) || x.RequestGoodsSupplyDetails.Any(x => EF.Functions.Like(x.ConsumableVolumeProduct.ProjectOperationDetail.Description, filterDescription.MakeLikePattern()))) &&
                (string.IsNullOrWhiteSpace(requestNumber) || EF.Functions.Like(x.RequestGoodsSupply.SerialNumber.ToString() + "-" + x.RequestGoodsSupply.Id.ToString(), requestNumber.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.RequestGoodsSupply.SerialNumber.ToString() + "-" + x.RequestGoodsSupply.Id.ToString(), filterData.MakeLikePattern())))

            .Select(x => new GetRequestGoodsSupplyProductByIdResponse
            {
                Id = x.Id,
                RequestGoodsSupplyId = x.RequestGoodsSupply.Id,
                IsPettyCash = x.RequestGoodsSupply.IsPettyCash,
                PurchaseLocation = x.RequestGoodsSupply.PurchaseLocation,
                SerialNumber = x.RequestGoodsSupply.SerialNumber,
                RequestNumber = $"{x.RequestGoodsSupply.SerialNumber}-{x.RequestGoodsSupply.Id}",
                CostCenterId = x.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,
                CostCenterName = x.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
                ProjectId = x.RequestGoodsSupply.ProjectOperation.Project.Id,
                ProjectName = x.RequestGoodsSupply.ProjectOperation.Project.ProjectName,
                ProjectManagerId = x.RequestGoodsSupply.ProjectOperation.Project.ProjectManager,
                ProjectOperationId = x.RequestGoodsSupply.ProjectOperation.Id,
                ProjectOperationMeasureId = x.RequestGoodsSupply.ProjectOperation.UnitOfMeasurementId,
                ProjectOperationName = x.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoName,
                ProjectOperationCode = x.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoCode,
                Workload = x.RequestGoodsSupply.ProjectOperation.Workload,
                CategoryId = x.RequestGoodsSupply.OperationInfoSeason.Season.Branch.Category.Id,
                CategoryName = x.RequestGoodsSupply.OperationInfoSeason.Season.Branch.Category.CategoryName,
                BranchId = x.RequestGoodsSupply.OperationInfoSeason.Season.Branch.Id,
                BranchName = x.RequestGoodsSupply.OperationInfoSeason.Season.Branch.BranchName,
                SeasonId = x.RequestGoodsSupply.OperationInfoSeason.Season.Id,
                SeasonName = x.RequestGoodsSupply.OperationInfoSeason.Season.SeasonName,
                ProjectOperationDetailIds = x.RequestGoodsSupplyDetails.Select(d => d.ConsumableVolumeProduct.ProjectOperationDetail.Id).ToList(),
                GetsProjectOperationDetailName = x.RequestGoodsSupplyDetails.Select(d => d.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PrivateName).ToList(),
                GetsProjectOperationDetailCode = x.RequestGoodsSupplyDetails.Select(d => d.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PrivateCode).ToList(),
                Importance = x.Importance,
                Status = x.Status,
                Type = x.RequestGoodsSupply.Type,
                ProductId = x.ProductId,
                RequestedCount = x.RequestedCount,
                SupplyCount = x.RequestGoodsSupplyManagements.Where(z => z.Status == GoodsSupplyManagementStatus.CompleteSupply).Sum(z => z.RequestedCount),
                DelivaryDeadLine = x.DelivaryDeadLine,
                RequestedDate = x.RequestGoodsSupply.RequestedDate,
                Created = x.RequestGoodsSupply.Created,
                UnitPrice = x.UnitPrice,
                TotalPrice = x.TotalPrice,
                TaxPercentage = x.TaxPercentage,
                TaxNumber = x.TaxNumber,
                DiscountByPercentage = x.DiscountByPercentage,
                DiscountByNumber = x.DiscountByNumber,
                DiscountedPrice = x.DiscountedPrice,
                PackingPrice = 0,
                TransferPrice = x.TransferPrice,
                OtherPrice = x.RequestGoodsSupply.OtherPrice,
                FinalPrice = x.FinalPrice,
                CheckGroup = x.CheckGroup,
                ContractorId = x.ContractorId,
                SupplyerId = x.RequestGoodsSupply.SupplyerId,
                BuyerId = x.RequestGoodsSupply.BuyerId,
                CurrencyId = x.RequestGoodsSupply.CurrencyId,
                DestinationWarehouseId = x.DestinationWarehouseId,
                CreatorId = x.RequestGoodsSupply.CreatorId,
                PackageId = x.PackageId,
                PackageCount = x.PackageCount,
                PackageUnitPrice = x.PackageUnitPrice,
                CustomerInvoiceNumber = x.CustomerInvoiceNumber,
                Description = x.Description,
                ManagementDescription = x.ManagementDescription,
                LastDescription = x.LastDescription,
                Managements = x.RequestGoodsSupplyManagements.Select(z => new GetsRequestGoodsSupplyProductManagement()
                {
                    Id = z.Id,
                    Type = z.Type,
                    Status = z.Status,
                    InvoiceId = z.InvoiceId,
                    WarehouseId = z.WarehouseId,
                    DestinationWarehouseId = z.DestinationWarehouseId,
                    ProductId = z.ReferenceId,
                    RequestedCount = z.RequestedCount,
                    ConfirmedRequestCount = z.ConfirmedRequestCount,
                    AlternateId = z.AlternateId,
                    OperatorAppointmentId = z.OperatorAppointmentId,
                    Description = z.Description,
                    LastDescription = z.LastDescription,
                    AssignmentDate = z.AssignmentDate,
                }).ToList(),
            });

        return newQuery;
    }

    public async Task<(List<GetsGoodsSupplyDetailBySupplyProductIdWithScaleModel> Data, int RowCount)> GetsGoodsSupplyDetailBySupplyProductIdWithScale(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? productIds,
        List<long>? creatorIds,
        List<long>? warehouseIds,
        long? cityId,
        long? projectManagerId,
        List<GoodsSupplyDetailImportance>? importances,
        List<GoodsSupplyType>? types,
        List<GoodsSupplyDetailStatus>? statuses,
        List<GoodsSupplyDetailStatus>? removeStatuses,
        string? requestNumber,
        string? filterDescription,
        string? filterPublicName,
        string? filterOperationInfoName,
        string? filterManagerDescription,
        string? filterData,
        string? customerInvoiceNumber,
        string? filterProduct,
        DateTime? fromDate,
        DateTime? toDate,
        int pageIndex,
        int pageSize,
       CT ct)
    {
        var query = DbSet
              .Where(x => !x.IsDeleted && !x.RequestGoodsSupply.IsDeleted &&
                (ids == null || ids.Contains(x.Id)) &&
                (costCenterIds == null || x.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (cityId == null || x.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenter.CityId == cityId)) &&
                (projectIds == null || projectIds.Contains(x.RequestGoodsSupply.ProjectOperation.Project.Id)) &&
                (projectManagerId == null || x.RequestGoodsSupply.ProjectOperation.Project.ProjectManager.Equals(projectManagerId)) &&
                (projectOperationIds == null || projectOperationIds.Contains(x.RequestGoodsSupply.ProjectOperation.Id)) &&
                (projectOperationDetailIds == null || x.RequestGoodsSupplyDetails.Any(d => projectOperationDetailIds.Contains(d.ConsumableVolumeProduct.ProjectOperationDetail.Id))) &&
                (productIds == null || productIds.Count == 0 || productIds.Contains(x.ProductId)) &&
                (creatorIds == null || creatorIds.Contains(x.RequestGoodsSupply.CreatorId)) &&
                (warehouseIds == null || x.RequestGoodsSupply.Type == GoodsSupplyType.Project && warehouseIds.Contains(x.DestinationWarehouseId!.Value) ||
                 x.RequestGoodsSupplyManagements.Any(m => m.WarehouseId.HasValue && warehouseIds.Contains(m.WarehouseId.Value)) ||
                 x.RequestGoodsSupplyManagements.Any(m => m.DestinationWarehouseId.HasValue && warehouseIds.Contains(m.DestinationWarehouseId.Value))) &&
                (fromDate == null || x.RequestGoodsSupply.Created.Date >= fromDate.Value.Date) &&
                (toDate == null || x.RequestGoodsSupply.Created.Date <= toDate.Value.Date) &&
                (types == null || types.Contains(x.RequestGoodsSupply.Type)) &&
                (statuses == null || statuses.Contains(x.Status)) &&
                (removeStatuses == null || !removeStatuses.Contains(x.Status)) &&
                (customerInvoiceNumber == null || x.CustomerInvoiceNumber == customerInvoiceNumber) &&
                (string.IsNullOrWhiteSpace(filterManagerDescription) || EF.Functions.Like(x.ManagementDescription, filterManagerDescription.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterOperationInfoName) || EF.Functions.Like(x.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoName, filterOperationInfoName.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterPublicName) || x.RequestGoodsSupplyDetails.Any(x => EF.Functions.Like(x.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PublicName, filterPublicName.MakeLikePattern()))) &&
                (string.IsNullOrWhiteSpace(filterDescription) || x.RequestGoodsSupplyDetails.Any(x => EF.Functions.Like(x.ConsumableVolumeProduct.ProjectOperationDetail.Description, filterDescription.MakeLikePattern()))) &&
                (string.IsNullOrWhiteSpace(requestNumber) || EF.Functions.Like(x.RequestGoodsSupply.SerialNumber.ToString() + "-" + x.RequestGoodsSupply.Id.ToString(), requestNumber.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                 EF.Functions.Like(x.SerialNumber.ToString() + "-" + x.Id.ToString(), filterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.ManagementDescription, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.Description, filterData.MakeLikePattern()))
                )
            .Select(x => new GetsGoodsSupplyDetailBySupplyProductIdWithScaleModel()
            {
                Id = x.Id,
                RequestGoodsSupplyId = x.RequestGoodsSupply.Id,
                IsPettyCash = x.RequestGoodsSupply.IsPettyCash,
                PurchaseLocation = x.RequestGoodsSupply.PurchaseLocation,
                PurchaseReason = x.RequestGoodsSupply.PurchaseReason,
                SerialNumber = x.RequestGoodsSupply.SerialNumber,
                RequestNumber = $"{x.RequestGoodsSupply.SerialNumber}-{x.RequestGoodsSupply.Id}",
                CostCenterId = x.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,
                CostCenterName = x.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
                ProjectId = x.RequestGoodsSupply.ProjectOperation.Project.Id,
                ProjectName = x.RequestGoodsSupply.ProjectOperation.Project.ProjectName,
                ProjectManagerId = x.RequestGoodsSupply.ProjectOperation.Project.ProjectManager,
                ProjectOperationId = x.RequestGoodsSupply.ProjectOperation.Id,
                ProjectOperationMeasureId = x.RequestGoodsSupply.ProjectOperation.UnitOfMeasurementId,
                ProjectOperationName = x.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoName,
                ProjectOperationCode = x.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoCode,
                Workload = x.RequestGoodsSupply.ProjectOperation.Workload,
                ProjectOperationDetailIds = x.RequestGoodsSupplyDetails.Select(d => d.ConsumableVolumeProduct.ProjectOperationDetail.Id).ToList(),
                GetsProjectOperationDetailName = x.RequestGoodsSupplyDetails.Select(d => d.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PrivateName).ToList(),
                GetsProjectOperationDetailCode = x.RequestGoodsSupplyDetails.Select(d => d.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PrivateCode).ToList(),
                Importance = x.Importance,
                Status = x.Status,
                Type = x.RequestGoodsSupply.Type,
                ProductId = x.ProductId,
                RequestedCount = x.RequestedCount,
                SupplyCount = x.RequestGoodsSupplyManagements.Where(z => z.Status == GoodsSupplyManagementStatus.CompleteSupply).Sum(z => z.RequestedCount),
                DelivaryDeadLine = x.DelivaryDeadLine,
                RequestedDate = x.RequestGoodsSupply.RequestedDate,
                Created = x.RequestGoodsSupply.Created,
                UnitPrice = x.UnitPrice,
                TotalPrice = x.TotalPrice,
                TaxPercentage = x.TaxPercentage,
                TaxNumber = x.TaxNumber,
                DiscountByPercentage = x.DiscountByPercentage,
                DiscountByNumber = x.DiscountByNumber,
                DiscountedPrice = x.DiscountedPrice,
                PackingPrice = 0,
                TransferPrice = x.TransferPrice,
                OtherPrice = x.RequestGoodsSupply.OtherPrice,
                FinalPrice = x.FinalPrice,
                CheckGroup = x.CheckGroup,
                ContractorId = x.ContractorId,
                SupplyerId = x.RequestGoodsSupply.SupplyerId,
                BuyerId = x.RequestGoodsSupply.BuyerId,
                CurrencyId = x.RequestGoodsSupply.CurrencyId,
                DestinationWarehouseId = x.DestinationWarehouseId,
                CreatorId = x.RequestGoodsSupply.CreatorId,
                PackageId = x.PackageId,
                PackageCount = x.PackageCount,
                PackageUnitPrice = x.PackageUnitPrice,
                CustomerInvoiceNumber = x.CustomerInvoiceNumber,
                Description = x.Description,
                ManagementDescription = x.ManagementDescription,
                LastDescription = x.LastDescription
            });

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<long> GetProjectManagerId(
        long id, CT ct)
    {
        return await DbSet.Where(x => x.Id == id)
            .Select(x => x.RequestGoodsSupply.IsProjectSupply ?
            x.RequestGoodsSupply.Project.ProjectManager.Value :
            x.RequestGoodsSupply.ProjectOperation.Project.ProjectManager.Value).FirstOrDefaultAsync(ct);
    }
}

#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.