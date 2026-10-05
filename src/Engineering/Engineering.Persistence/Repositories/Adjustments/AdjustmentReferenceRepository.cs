using Engineering.Application.Abstractions.Data.Adjustments;
using Engineering.Domain.Entities.Adjustments;

namespace Engineering.Persistence.Repositories.Adjustments;

public class AdjustmentReferenceRepository
    : BaseRepository<EngineeringDBContext, AdjustmentReference>,
      IAdjustmentReferenceRepository
{
    public AdjustmentReferenceRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<AdjustmentReference?> GetByTitle(string title, CT ct)
    {
        return await DbSet
         .FirstOrDefaultAsync(x => x.Title == title, ct);
    }

    public async Task<AdjustmentReference?> GetById(long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }
}