using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductHistories;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Abstractions.Data.FiduciaryProducts;

public interface IFiduciaryProductHistoryRepository : IBaseRepository<FiduciaryProductHistory>
{
    Task<(List<GetFilteredFiduciaryProductHistoriesModel> Data, int RowCount)> GetFilteredFiduciaryProductHistories(long fiduciaryProductId, string[]? orderBy, int pageIndex, int pageSize, CT ct);
}
