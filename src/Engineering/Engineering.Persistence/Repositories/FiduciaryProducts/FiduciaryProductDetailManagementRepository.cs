using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Persistence.Repositories.FiduciaryProducts;

public class FiduciaryProductDetailManagementRepository : BaseRepository<EngineeringDBContext, FiduciaryProductDetailManagement>, IFiduciaryProductDetailManagementRepository
{
    public FiduciaryProductDetailManagementRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<List<FiduciaryProductDetailManagement>> GetByIdsAsync(List<long> ids, CT ct)
    {
        var query = DbSet.Include(oo => oo.FiduciaryProductDetail)
                            .ThenInclude(oo => oo.FiduciaryProduct)
                        .Include(oo => oo.FiduciaryProductDetail)
                            .ThenInclude(oo => oo.Managements)
                                .ThenInclude(oo => oo.Returns)
                          .Where(oo => ids.Contains(oo.Id))
            .OrderByDescending(oo => oo.Created);

        return await query.ToListAsync(ct);
    }
}
