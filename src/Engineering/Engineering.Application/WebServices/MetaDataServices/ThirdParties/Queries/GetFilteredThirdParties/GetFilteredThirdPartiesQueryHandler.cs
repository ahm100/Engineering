using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Contractors.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredThirdParties;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredThirdParties;

public class GetFilteredThirdPartiesQueryHandler : IQueryHandler<GetFilteredThirdPartiesQuery, DataResult<List<Contractor>>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetFilteredThirdPartiesQueryHandler> _logger;

    public GetFilteredThirdPartiesQueryHandler(ILogger<GetFilteredThirdPartiesQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<Contractor>>?>> Handle(GetFilteredThirdPartiesQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetFilteredThirdParties(request.Adapt<GetFilteredThirdPartiesRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<Contractor>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<Contractor>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Contractor>>>(SharedErrors.UnknownError);
        }
    }
}