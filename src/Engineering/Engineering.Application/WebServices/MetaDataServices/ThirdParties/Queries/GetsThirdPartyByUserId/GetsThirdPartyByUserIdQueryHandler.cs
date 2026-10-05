using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsThirdPartyByUserId;
using ThirdPartyModel = Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.ThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetsThirdPartyBuyUserId;

public class GetsThirdPartyByUserIdQueryHandler : IQueryHandler<GetsThirdPartyByUserIdQuery, DataResult<List<ThirdPartyModel>>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetsThirdPartyByUserIdQueryHandler> _logger;

    public GetsThirdPartyByUserIdQueryHandler(ILogger<GetsThirdPartyByUserIdQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<ThirdPartyModel>>?>> Handle(GetsThirdPartyByUserIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetsThirdPartyByUserId(request.Adapt<GetsThirdPartyByUserIdRequest>(), ct);

            return (result?.Data?.Any()) ?? false ?
                new DataResult<List<ThirdPartyModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ThirdPartyModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ThirdPartyModel>>>(SharedErrors.UnknownError);
        }
    }
}