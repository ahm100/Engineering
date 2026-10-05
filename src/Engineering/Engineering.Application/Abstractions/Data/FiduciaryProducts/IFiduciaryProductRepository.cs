using Engineering.Application.Services.FiduciaryProducts.Models.GetFiduciaryProductById;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Abstractions.Data.FiduciaryProducts;

public interface IFiduciaryProductRepository : IBaseRepository<FiduciaryProduct>
{
    Task<GetFiduciaryProductByIdResponse?> GetDataById(long id, CT ct);
    Task<FiduciaryProduct?> GetByIdAsync(long id, CT ct);
    Task<FiduciaryProduct?> GetFiduciaryProductForChangeStatus(long id, CT ct);

    Task<(List<GetFilteredFiduciaryProductsModel> Data, int RowCount)> GetFilteredAsync(
        List<long>? ids,
        long? costCenterId,
        long? projectId,
        FiduciaryProductStatus? status,
        List<long>? projectOperationIds,
        long? thirdPartyId,
        DateTime? fromDate,
        DateTime? toDate,
        string? FilterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<FiduciaryProduct>> GetFiduciaryProductsByDate(
        DateTime startDate,
        DateTime endDate,
        List<long> projectOperationIds,
        CT ct);
}
