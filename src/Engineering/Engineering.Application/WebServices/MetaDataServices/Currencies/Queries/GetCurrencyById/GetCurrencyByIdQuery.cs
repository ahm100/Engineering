using CurrencyModel = Engineering.Application.WebServices.MetaDataServices.Currencies.Models.Currency;

namespace Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetCurrencyById;

public record GetCurrencyByIdQuery(
    long Id
    ) : IQuery<CurrencyModel?>;
