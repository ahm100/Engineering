using Engineering.Application.Abstractions.Data.Synonyms.Meta.Currencies;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models;
using Engineering.Domain.Entities.Synonyms.MetaData.Currencies;

namespace Engineering.Persistence.Repositories.Synonyms.Meta.Currencies;

public class ViewCurrencyRepository : BaseRepository<EngineeringDBContext, ViewCurrency>, IViewCurrencyRepository
{
    public ViewCurrencyRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ViewCurrency?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<Currency>?> GetCurrenciesByIds(
    List<long> ids, CT ct)
    {
        return await DbSet
            .Where(x => ids.Contains(x.Id))
            .Select(x => new Currency(
                x.Id,
                x.Name,
                x.IsDefault,
                x.Iso,
                x.Symbol,
                x.CountryId
            ))
            .ToListAsync(ct);
    }
}