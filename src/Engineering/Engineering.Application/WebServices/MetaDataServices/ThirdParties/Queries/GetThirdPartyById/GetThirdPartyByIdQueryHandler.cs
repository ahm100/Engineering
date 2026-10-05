using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetThirdPartyById;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetThirdPartyById;

public class GetThirdPartyByIdQueryHandler : IQueryHandler<GetThirdPartyByIdQuery, ThirdPartyUserModel?>
{
    private readonly ILogger<GetThirdPartyByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetThirdPartyByIdQueryHandler(ILogger<GetThirdPartyByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<ThirdPartyUserModel?>> Handle(GetThirdPartyByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetThirdPartyById(request.Adapt<GetThirdPartyByIdRequest>(), ct);

            return result?.Value != null ? result.Value : Result.Failure<ThirdPartyUserModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ThirdPartyUserModel?>(SharedErrors.UnknownError);
        }
    }
}
