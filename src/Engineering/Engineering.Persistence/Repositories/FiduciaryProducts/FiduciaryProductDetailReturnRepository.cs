using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Persistence.Repositories.FiduciaryProducts;

public class FiduciaryProductDetailReturnRepository : BaseRepository<EngineeringDBContext, FiduciaryProductDetailReturn>, IFiduciaryProductDetailReturnRepository
{
    public FiduciaryProductDetailReturnRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<FiduciaryProductDetailReturn?> GetByIdAsync(long id, CT ct)
    {
        var query = DbSet.Include(oo => oo.Documents.Where(c => !c.IsDeleted))
                          .Where(oo => oo.Id.Equals(id))
            .OrderByDescending(oo => oo.Created);

        return await query.FirstOrDefaultAsync();
    }
}
