using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductHistories;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Persistence.Repositories.FiduciaryProducts;

public class FiduciaryProductHistoryRepository : BaseRepository<EngineeringDBContext, FiduciaryProductHistory>, IFiduciaryProductHistoryRepository
{
    public FiduciaryProductHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<GetFilteredFiduciaryProductHistoriesModel> Data, int RowCount)> GetFilteredFiduciaryProductHistories(
        long fiduciaryProductId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x => x.FiduciaryProduct.Id == fiduciaryProductId)
            .Select(x => new GetFilteredFiduciaryProductHistoriesModel()
            {
                Created = x.Created,
                CreatorId = x.CreatorId,
                Description = x.Description,
                Id = x.Id,
                LastDescription = x.LastDescription,
                RequestNumber = x.FiduciaryProduct.Id,
                Status = x.Status,
                StatusDescription = x.StatusDescription,
                ThirdPartyId = x.ThirdPartyId
            });

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);

        return (entities, count);
    }
}
