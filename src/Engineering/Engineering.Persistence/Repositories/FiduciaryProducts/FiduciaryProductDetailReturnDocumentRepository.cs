using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Persistence.Repositories.FiduciaryProducts;

public class FiduciaryProductDetailReturnDocumentRepository : BaseRepository<EngineeringDBContext, FiduciaryProductDetailReturnDocument>, IFiduciaryProductDetailReturnDocumentRepository
{
    public FiduciaryProductDetailReturnDocumentRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<FiduciaryProductDetailReturnDocument> Data, int RowCount)> GetFiltered(long fiduciaryProductDetailReturnId, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Include(oo => oo.FiduciaryProductDetailReturn)
                         .Where(oo => oo.FiduciaryProductDetailReturn.Id.Equals(fiduciaryProductDetailReturnId));

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
