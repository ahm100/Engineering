using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsThirdPartyById;
using ThirdPartyModel = Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.ThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetsThirdPartyById;

public class GetsThirdPartyByIdQueryHandler : IQueryHandler<GetsThirdPartyByIdQuery, DataResult<List<ThirdPartyModel?>?>?>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetsThirdPartyByIdQueryHandler> _logger;

    public GetsThirdPartyByIdQueryHandler(ILogger<GetsThirdPartyByIdQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<ThirdPartyModel?>?>?>> Handle(GetsThirdPartyByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetsThirdPartyById(request.Adapt<GetsThirdPartyByIdRequest>(), ct);

            return (result?.Value?.Data?.Any() ?? false) ?
                new DataResult<List<ThirdPartyModel?>?>
                {
                    Data = result.Value!.Data!,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<ThirdPartyModel?>?>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ThirdPartyModel?>?>>(SharedErrors.UnknownError);
        }
    }
}