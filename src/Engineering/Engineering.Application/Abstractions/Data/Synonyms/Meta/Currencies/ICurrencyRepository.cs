using Engineering.Application.WebServices.MetaDataServices.Currencies.Models;
using Engineering.Domain.Entities.Synonyms.MetaData.Currencies;

namespace Engineering.Application.Abstractions.Data.Synonyms.Meta.Currencies;

public interface IViewCurrencyRepository : IBaseRepository<ViewCurrency>
{
    Task<ViewCurrency?> GetById(
        long id, CT ct);

    Task<List<Currency>?> GetCurrenciesByIds(
    List<long> ids, CT ct);
}