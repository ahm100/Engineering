using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Cities.Models.GetsCityById;
using CityModel = Engineering.Application.WebServices.MetaDataServices.Cities.Models.City;

namespace Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetsCityById;

public class GetsCityByIdQueryHandler : IQueryHandler<GetsCityByIdQuery, DataResult<List<CityModel>>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetsCityByIdQueryHandler> _logger;

    public GetsCityByIdQueryHandler(ILogger<GetsCityByIdQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<CityModel>>?>> Handle(GetsCityByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetsCityById(request.Adapt<GetsCityByIdRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<CityModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<CityModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<CityModel>>>(SharedErrors.UnknownError);
        }
    }
}