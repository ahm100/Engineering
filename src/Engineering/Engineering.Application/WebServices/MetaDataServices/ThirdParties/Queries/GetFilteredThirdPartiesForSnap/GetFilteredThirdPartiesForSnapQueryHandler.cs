using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredThirdPartiesForSnap;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredThirdPartiesForSnap;

public class GetFilteredThirdPartiesForSnapQueryHandler : IQueryHandler<GetFilteredThirdPartiesForSnapQuery, DataResult<List<GetFilteredForSnapModel>>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetFilteredThirdPartiesForSnapQueryHandler> _logger;

    public GetFilteredThirdPartiesForSnapQueryHandler(ILogger<GetFilteredThirdPartiesForSnapQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<GetFilteredForSnapModel>>?>> Handle(GetFilteredThirdPartiesForSnapQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetFilteredThirdPartiesForSnap(request.Adapt<GetFilteredThirdPartiesForSnapRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<GetFilteredForSnapModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GetFilteredForSnapModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetFilteredForSnapModel>>>(SharedErrors.UnknownError);
        }
    }
}