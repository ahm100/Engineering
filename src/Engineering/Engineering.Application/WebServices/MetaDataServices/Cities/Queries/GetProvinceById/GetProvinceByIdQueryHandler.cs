using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Cities.Models.GetProvinceById;

namespace Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetProvinceById;

public class GetProvinceByIdQueryHandler : IQueryHandler<GetProvinceByIdQuery, GetProvinceByIdResponse?>
{
    private readonly ILogger<GetProvinceByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetProvinceByIdQueryHandler(ILogger<GetProvinceByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<GetProvinceByIdResponse?>> Handle(GetProvinceByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetProvinceById(request.Adapt<GetProvinceByIdRequest>(), ct);

            return result ?? Result.Failure<GetProvinceByIdResponse?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetProvinceByIdResponse?>(SharedErrors.UnknownError);
        }
    }
}