using Engineering.Domain.Entities.Synonyms.MetaData.Currencies;
using Engineering.Domain.Entities.Synonyms.MetaData.FinancialPeriod;

namespace Engineering.Application.Abstractions.Data.Synonyms.FinanicalPeriod;

public interface IViewFinancialPeriodRepository : IBaseRepository<ViewFinancialPeriod>
{
    Task<ViewFinancialPeriod?> GetByYearId(long yearId, CT ct);

}
