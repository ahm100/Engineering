using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetCurrencyById;
using CurrencyModel = Engineering.Application.WebServices.MetaDataServices.Currencies.Models.Currency;

namespace Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetCurrencyById;

public class GetCurrencyByIdQueryHandler : IQueryHandler<GetCurrencyByIdQuery, CurrencyModel?>
{
    private readonly ILogger<GetCurrencyByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetCurrencyByIdQueryHandler(ILogger<GetCurrencyByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<CurrencyModel?>> Handle(GetCurrencyByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetCurrencyById(request.Adapt<GetCurrencyByIdRequest>(), ct);

            return result?.Value ?? Result.Failure<CurrencyModel?>(MetaDataErrors.CurrencyWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CurrencyModel?>(SharedErrors.UnknownError);
        }
    }
}