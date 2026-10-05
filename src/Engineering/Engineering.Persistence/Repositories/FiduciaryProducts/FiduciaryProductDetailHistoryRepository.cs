using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductDetailHistories;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Persistence.Repositories.FiduciaryProducts;

public class FiduciaryProductDetailHistoryRepository : BaseRepository<EngineeringDBContext, FiduciaryProductDetailHistory>, IFiduciaryProductDetailHistoryRepository
{
    public FiduciaryProductDetailHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<GetFilteredFiduciaryProductDetailHistoriesModel> Data, int RowCount)> GetFilteredFiduciaryProductDetailHistories(
        long fiduciaryProductId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(oo => oo.FiduciaryProductDetail.FiduciaryProduct.Id == fiduciaryProductId)
            .Select(x => new GetFilteredFiduciaryProductDetailHistoriesModel()
            {
                Created = x.Created,
                CreatorId = x.CreatorId,
                CurrencyId = x.CurrencyId,
                DailyLateFine = x.DailyLateFine,
                Id = x.Id,
                LastDescription = x.LastDescription,
                LoanCount = x.LoanCount,
                LoanDays = x.LoanDays,
                MeasureunitId = x.MeasureUnitId,
                ProductDescription = x.Description,
                ProductId = x.ProductId,
                Status = x.Status,
                StatusDescription = x.StatusDescription,
                WarehouseIds = x.FiduciaryProductDetail.Managements.Select(z => z.WarehouseId).ToList(),
            });

        query = query.OrderBy(x => x.ProductId)
                     .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);

        return (entities, count);
    }
}
