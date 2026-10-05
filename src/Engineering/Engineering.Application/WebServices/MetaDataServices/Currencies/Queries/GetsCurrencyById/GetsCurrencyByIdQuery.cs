using CurrencyModel = Engineering.Application.WebServices.MetaDataServices.Currencies.Models.Currency;

namespace Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetsCurrencyById;

public record GetsCurrencyByIdQuery(
    int PageIndex,
    int PageSize,
    List<long> Ids,
    bool IgnoreQuery
    ) : IQuery<DataResult<List<CurrencyModel>>>;
