
namespace Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetsCurrencyById;

public record GetsCurrencyByIdRequest(
    int PageIndex,
    int PageSize,
    List<long> Ids,
    bool IgnoreQuery
    );
