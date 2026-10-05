using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetsCurrencyById;
using CurrencyModel = Engineering.Application.WebServices.MetaDataServices.Currencies.Models.Currency;

namespace Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetsCurrencyById;

public class GetsCurrencyByIdQueryHandler : IQueryHandler<GetsCurrencyByIdQuery, DataResult<List<CurrencyModel>>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetsCurrencyByIdQueryHandler> _logger;

    public GetsCurrencyByIdQueryHandler(ILogger<GetsCurrencyByIdQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<CurrencyModel>>?>> Handle(GetsCurrencyByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetsCurrencyById(request.Adapt<GetsCurrencyByIdRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<CurrencyModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<CurrencyModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<CurrencyModel>>>(SharedErrors.UnknownError);
        }
    }
}