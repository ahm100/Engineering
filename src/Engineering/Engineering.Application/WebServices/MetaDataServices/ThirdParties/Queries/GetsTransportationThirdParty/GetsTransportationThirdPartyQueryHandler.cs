using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsTransportationThirdParty;

namespace Engineering.Application.WebServices.MetaData.ThirdParties.Queries.GetsTransportationThirdParty;

public class GetsTransportationThirdPartyQueryHandler : IQueryHandler<GetsTransportationThirdPartyQuery, List<GetsTransportationThirdPartyModel>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetsTransportationThirdPartyQueryHandler> _logger;

    public GetsTransportationThirdPartyQueryHandler(ILogger<GetsTransportationThirdPartyQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<List<GetsTransportationThirdPartyModel>?>> Handle(GetsTransportationThirdPartyQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetsTransportationThirdParty(request.Adapt<GetsTransportationThirdPartyRequest>(), ct);

            return result.Adapt<List<GetsTransportationThirdPartyModel>>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetsTransportationThirdPartyModel>>(SharedErrors.UnknownError);
        }
    }
}