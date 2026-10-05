using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Directors.Models.GetDirectorById;
using DirectorModel = Engineering.Application.WebServices.MetaDataServices.Directors.Models.Director;

namespace Engineering.Application.WebServices.MetaDataServices.Directors.Queries.GetDirectorById;

public class GetDirectorByIdQueryHandler : IQueryHandler<GetDirectorByIdQuery, DirectorModel?>
{
    private readonly ILogger<GetDirectorByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetDirectorByIdQueryHandler(ILogger<GetDirectorByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<DirectorModel?>> Handle(GetDirectorByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetDirectorById(request.Adapt<GetDirectorByIdRequest>(), ct);

            var data = result?.Data!.FirstOrDefault();

            return data ?? Result.Failure<DirectorModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DirectorModel?>(SharedErrors.UnknownError);
        }
    }
}