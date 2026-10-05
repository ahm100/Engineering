using Engineering.Application.Abstractions.Data.Synonyms.FinanicalPeriod;
using Engineering.Domain.Entities.Synonyms.MetaData.FinancialPeriod;

namespace Engineering.Persistence.Repositories.Synonyms.FinancialPeriod;

public class ViewFinancialPeriodRepository : BaseRepository<EngineeringDBContext, ViewFinancialPeriod>, IViewFinancialPeriodRepository
{
    public ViewFinancialPeriodRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ViewFinancialPeriod?> GetByYearId(long yearId, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == yearId, ct);
    }
}
