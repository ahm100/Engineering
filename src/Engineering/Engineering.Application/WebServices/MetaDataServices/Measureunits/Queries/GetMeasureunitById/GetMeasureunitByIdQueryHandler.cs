using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Models.GetMeasureunitById;
using MeasureunitModel = Engineering.Application.WebServices.MetaDataServices.Measureunits.Models.Measureunit;

namespace Engineering.Application.WebServices.MetaDataServices.Measureunits.Queries.GetMeasureunitById;

public class GetMeasureunitByIdQueryHandler : IQueryHandler<GetMeasureunitByIdQuery, MeasureunitModel?>
{
    private readonly ILogger<GetMeasureunitByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetMeasureunitByIdQueryHandler(ILogger<GetMeasureunitByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<MeasureunitModel?>> Handle(GetMeasureunitByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetMeasureunitById(request.Adapt<GetMeasureunitByIdRequest>(), ct);

            return result?.Value ?? Result.Failure<MeasureunitModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MeasureunitModel?>(SharedErrors.UnknownError);
        }
    }
}