using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Cities.Models.GetCityById;
using Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetCityById;
using CityModel = Engineering.Application.WebServices.MetaDataServices.Cities.Models.City;

namespace Engineering.Application.WebServices.MetaDataServices.Citys.Queries.GetCityById;

public class GetCityByIdQueryHandler : IQueryHandler<GetCityByIdQuery, CityModel?>
{
    private readonly ILogger<GetCityByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetCityByIdQueryHandler(ILogger<GetCityByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<CityModel?>> Handle(GetCityByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetCityById(request.Adapt<GetCityByIdRequest>(), ct);

            return result?.Value ?? Result.Failure<CityModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CityModel?>(SharedErrors.UnknownError);
        }
    }
}