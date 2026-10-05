using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductDetailHistories;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Abstractions.Data.FiduciaryProducts;

public interface IFiduciaryProductDetailHistoryRepository : IBaseRepository<FiduciaryProductDetailHistory>
{
    Task<(List<GetFilteredFiduciaryProductDetailHistoriesModel> Data, int RowCount)> GetFilteredFiduciaryProductDetailHistories(
        long fiduciaryProductDetailId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);
}
