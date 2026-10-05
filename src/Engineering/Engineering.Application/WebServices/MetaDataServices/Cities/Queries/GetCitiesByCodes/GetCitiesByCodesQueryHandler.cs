using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetCitiesByCodes;

namespace Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetCitiesByCodes;

public class GetCitiesByCodesQueryHandler : IQueryHandler<GetCitiesByCodesQuery, DataResult<List<GetsCityByCodesModel>>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetCitiesByCodesQueryHandler> _logger;

    public GetCitiesByCodesQueryHandler(ILogger<GetCitiesByCodesQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<GetsCityByCodesModel>>?>> Handle(GetCitiesByCodesQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetCitiesByCodes(request.Adapt<GetCitiesByCodesRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<GetsCityByCodesModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GetsCityByCodesModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsCityByCodesModel>>>(SharedErrors.UnknownError);
        }
    }
}