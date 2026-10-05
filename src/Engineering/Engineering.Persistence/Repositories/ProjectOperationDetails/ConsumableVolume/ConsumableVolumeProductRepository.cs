using DocumentFormat.OpenXml.Wordprocessing;
using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.DataModels;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetTotalSupplyByProjectOperationDetailIds;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Persistence.Repositories.ProjectOperationDetails.ConsumableVolume;

public class ConsumableVolumeProductRepository : BaseRepository<EngineeringDBContext, ConsumableVolumeProduct>, IConsumableVolumeProductRepository
{
    public ConsumableVolumeProductRepository(EngineeringDBContext context) : base(context) { }

    public async Task<ConsumableVolumeProduct?> GetById(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperationDetail.ProjectOperation.OperationInfo.ConsumptionStandardProduct)
            .Include(x => x.RequestGoodsSupplyDetails)
                .Where(oo => oo.Id == id && !oo.ProjectOperationDetail.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<List<ConsumableVolumeProduct>?> GetByIds(
        List<long> ids,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperationDetail.ProjectOperation.OperationInfo.ConsumptionStandardProduct)
            .Include(x => x.RequestGoodsSupplyDetails)
                .Where(oo => ids.Contains(oo.Id) && !oo.ProjectOperationDetail.IsDeleted);

        var result = await query.ToListAsync(ct);
        return result;
    }

    public async Task<(List<ConsumableVolumeProduct> Data, int RowCount)> GetsByProjectOperationDetailId(
        long id,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(oo => oo.ProjectOperationDetail!.Id == id && oo.IsDeleted != true)
            .Include(oo => oo.ProjectOperationDetail);

        var count = await query.CountAsync(ct);
        var items = await query.Page(pageIndex, pageSize).ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProductsDataModel> Data, int RowCount)> GetsByProjectOperationId(
        long projectOperationId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(oo => oo.ProjectOperationDetail!.ProjectOperation.Id == projectOperationId && oo.IsDeleted != true)
            .Include(oo => oo.ProjectOperationDetail)
                .ThenInclude(oo => oo.ConsumableVolumeProducts)
            .Select(e => new ProductsDataModel
            {
                Id = e.Id,
                ProjectOperationDetailId = e.ProjectOperationDetail.Id,
                ProductGroupId = e.ProductGroupId,
                UnusedPercentage = e.UnusedPercentage,
                IsStandard = e.IsStandard,
                StandardValue = e.StandardValue,
                FinalValue = e.FinalValue,
                VolumeProductType = e.VolumeProductType,
            });

        var count = await query.CountAsync(ct);
        var items = await query.Page(pageIndex, pageSize).ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<ConsumableVolumeProduct>> GetProjectOperationDetailProducts(
        long projectOperationDetailId,
        long? productGroupId,
        CT ct)
    {
        var query = DbSet
            .Include(c => c.ProjectOperationDetail.OperationLocation)
            .Include(c => c.ProjectOperationDetail.ProjectOperation)
            .Include(x => x.RequestGoodsSupplyDetails)
            .ThenInclude(x => x.RequestGoodsSupplyManagements)

                .Where(c =>
                    c.ProjectOperationDetail.Id.Equals(projectOperationDetailId) &&
                    (productGroupId == null || c.ProductGroupId.Equals(productGroupId)) && !c.IsDeleted);

        return await query.ToListAsync(ct);
    }


    public async Task<List<ConsumableVolumeProduct>> GetsProductsByFiltered(
        long? costCenterId,
        long? projectId,
        List<long>? projectOperationIds,
        List<long>? productOperationDetailIds,
        CT ct)
    {
        var query = DbSet
            .Include(c => c.ProjectOperationDetail.ProjectOperation)
                .Where(c => !c.IsDeleted &&
                    (costCenterId == null || c.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                    (projectId == null || c.ProjectOperationDetail.ProjectOperation.Project.Id.Equals(projectId)) &&
                    (projectOperationIds == null || projectOperationIds.Contains(c.ProjectOperationDetail.ProjectOperation.Id)) &&
                    (productOperationDetailIds == null || productOperationDetailIds.Contains(c.ProjectOperationDetail.Id)));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<(List<ConsumableVolumeProduct> Data, int RowCount)> GetsConsumableVolumeProductsForSupply(
        VolumeProductType? type,
        long? projectOperationId,
        long? projectOperationDetailId,
        long? productGroupId,
        long? contractorId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(c => c.ProjectOperationDetail.OperationLocation)
            .Include(c => c.RequestGoodsSupplyDetails)
                .ThenInclude(c => c.RequestGoodsSupplyProduct)
                    .ThenInclude(c => c.RequestGoodsSupplyManagements)

            .Where(c =>
                (type == null || c.VolumeProductType.Equals(type)) &&
                (projectOperationId == null || c.ProjectOperationDetail.ProjectOperation.Id.Equals(projectOperationId)) &&
                (projectOperationDetailId == null || c.ProjectOperationDetail.Id.Equals(projectOperationDetailId)) &&
                (productGroupId == null || c.ProductGroupId.Equals(productGroupId)) &&
                (contractorId == null || c.ProjectOperationDetail.ProjectOperationDetailContractorServices.Any(x => x.ContractorId != null && x.ContractorId == contractorId)) &&
                !c.IsDeleted);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageSize > 0 || pageIndex > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ConsumableVolumeProduct> Data, int RowCount)> GetsConsumableProductForSupply(
        VolumeProductType? type,
        long? projectOperationId,
        long? projectOperationDetailId,
        long? productGroupId,
        long? contractorId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet

            .Where(c =>
                (type == null || c.VolumeProductType.Equals(type)) &&
                (projectOperationId == null || c.ProjectOperationDetail.ProjectOperation.Id.Equals(projectOperationId)) &&
                (projectOperationDetailId == null || c.ProjectOperationDetail.Id.Equals(projectOperationDetailId)) &&
                (productGroupId == null || c.ProductGroupId.Equals(productGroupId)) &&
                (contractorId == null || c.ProjectOperationDetail.ProjectOperationDetailContractorServices.Any(x => x.ContractorId != null && x.ContractorId == contractorId)) &&
                !c.IsDeleted);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageSize > 0 || pageIndex > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<GetTotalSupplyByProjectOperationDetailIdsModel>> GetTotalSupplyByProjectOperationDetailIdsAsync(
        List<long>? productOperationDetailIds,
        long productGroupId,
        CT ct)
    {
        return await DbSet
            .Where(c =>
                c.VolumeProductType == VolumeProductType.ProductGroup &&
                c.ProductGroupId.Equals(productGroupId) &&
                (productOperationDetailIds == null || productOperationDetailIds!.Contains(c.ProjectOperationDetail.Id)) &&
                !c.IsDeleted)
            .GroupBy(g => g.ProjectOperationDetail.Id)
            .Select(s => new GetTotalSupplyByProjectOperationDetailIdsModel
            {
                ProductOperationDetailId = s.Key,

                TotalSupplyCount = s.SelectMany(c => c.RequestGoodsSupplyDetails.Where(oo =>
                    oo.Status == GoodsSupplyDetailStatus.CompleteSupply)).Select(c => c.RequestedCount).Sum(),

                RequestedCount = s.SelectMany(c => c.RequestGoodsSupplyDetails.Where(oo =>
                    !GSDSRules.TotalSupply.Contains(oo.Status)
                )).Select(c => c.RequestedCount).Sum()

            }).ToListAsync(ct);
    }

    public async Task<List<GetTotalSupplyByProjectOperationDetailIdsModel>> GetTotalSupplyByProjectOperationDetailIdsAndCategoryIdAsync(
        List<long>? productOperationDetailIds,
        long categoryId,
        CT ct)
    {
        return await DbSet
            .Where(c =>
                c.VolumeProductType == VolumeProductType.Category &&
                c.ProductGroupId.Equals(categoryId) &&
                (productOperationDetailIds == null || productOperationDetailIds!.Contains(c.ProjectOperationDetail.Id)) &&
                !c.IsDeleted)
            .GroupBy(g => g.ProjectOperationDetail.Id)
            .Select(s => new GetTotalSupplyByProjectOperationDetailIdsModel
            {
                ProductOperationDetailId = s.Key,

                TotalSupplyCount = s.SelectMany(c => c.RequestGoodsSupplyDetails.Where(oo =>
                    oo.Status == GoodsSupplyDetailStatus.CompleteSupply)).Select(c => c.RequestedCount).Sum(),

                RequestedCount = s.SelectMany(c => c.RequestGoodsSupplyDetails.Where(oo =>
                    !GSDSRules.TotalSupply.Contains(oo.Status)
                )).Select(c => c.RequestedCount).Sum()

            }).ToListAsync(ct);
    }
}