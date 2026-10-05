using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Planers.Models.GetPlanerById;
using PlanerModel = Engineering.Application.WebServices.MetaDataServices.Planers.Models.Planer;

namespace Engineering.Application.WebServices.MetaDataServices.Planers.Queries.GetPlanerById;

public class GetPlanerByIdQueryHandler : IQueryHandler<GetPlanerByIdQuery, PlanerModel?>
{
    private readonly ILogger<GetPlanerByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetPlanerByIdQueryHandler(ILogger<GetPlanerByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<PlanerModel?>> Handle(GetPlanerByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetPlanerById(request.Adapt<GetPlanerByIdRequest>(), ct);

            var data = result?.Data!.FirstOrDefault();

            return data ?? Result.Failure<PlanerModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<PlanerModel?>(SharedErrors.UnknownError);
        }
    }
}