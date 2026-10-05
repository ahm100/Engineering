using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredByIds;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredByIds;

public class GetFilteredByIdsQueryHandler : IQueryHandler<GetFilteredByIdsQuery, DataResult<List<FilteredUserModel?>?>?>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetFilteredByIdsQueryHandler> _logger;

    public GetFilteredByIdsQueryHandler(ILogger<GetFilteredByIdsQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }
    public async Task<Result<DataResult<List<FilteredUserModel?>?>?>> Handle(GetFilteredByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetFilteredByIds(request.Adapt<GetFilteredByIdsRequest>(), ct);

            return (result.Value?.Data?.Any() ?? false) ?
                new DataResult<List<FilteredUserModel?>?>
                {
                    Data = result.Value!.Data!,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<FilteredUserModel?>?>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<FilteredUserModel?>?>>(SharedErrors.UnknownError);
        }
    }
}
