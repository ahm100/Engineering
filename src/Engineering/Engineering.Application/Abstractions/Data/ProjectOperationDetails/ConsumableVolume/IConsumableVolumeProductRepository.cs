using Engineering.Application.Services.ConsumableVolumes.Models.Products.DataModels;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetTotalSupplyByProjectOperationDetailIds;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;

public interface IConsumableVolumeProductRepository : IBaseRepository<ConsumableVolumeProduct>
{
    Task<ConsumableVolumeProduct?> GetById(
        long id,
        CT ct);

    Task<List<ConsumableVolumeProduct>?> GetByIds(
        List<long> ids,
        CT ct);

    Task<(List<ConsumableVolumeProduct> Data, int RowCount)> GetsByProjectOperationDetailId(
        long id,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProductsDataModel> Data, int RowCount)> GetsByProjectOperationId(
        long projectOperationId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<ConsumableVolumeProduct>> GetProjectOperationDetailProducts(
        long projectOperationDetailId,
        long? productGroupId,
        CT ct);

    Task<(List<ConsumableVolumeProduct> Data, int RowCount)> GetsConsumableVolumeProductsForSupply(
        VolumeProductType? type,
        long? projectOperationId,
        long? projectOperationDetailId,
        long? productGroupId,
        long? contractorId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ConsumableVolumeProduct> Data, int RowCount)> GetsConsumableProductForSupply(
        VolumeProductType? type,
        long? projectOperationId,
        long? projectOperationDetailId,
        long? productGroupId,
        long? contractorId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<ConsumableVolumeProduct>> GetsProductsByFiltered(
        long? costCenterId,
        long? projectId,
        List<long>? projectOperationIds,
        List<long>? productOperationDetailIds,
        CT ct);

    Task<List<GetTotalSupplyByProjectOperationDetailIdsModel>> GetTotalSupplyByProjectOperationDetailIdsAsync(
        List<long>? productOperationDetailIds,
        long ProductGroupId,
        CT ct);

    Task<List<GetTotalSupplyByProjectOperationDetailIdsModel>> GetTotalSupplyByProjectOperationDetailIdsAndCategoryIdAsync(
        List<long>? productOperationDetailIds,
        long categoryId,
        CT ct);
}
