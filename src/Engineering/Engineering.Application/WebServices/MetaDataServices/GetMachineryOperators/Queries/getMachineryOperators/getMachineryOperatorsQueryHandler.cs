using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.GetMachineryOperators.Models.GetMachineryOperators;

namespace Engineering.Application.WebServices.MetaDataServices.GetMachineryOperators.Queries.GetMachineryOperators;

public class GetMachineryOperatorsQueryHandler : IQueryHandler<GetMachineryOperatorsQuery, DataResult<List<OfficerModel?>?>?>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetMachineryOperatorsQueryHandler> _logger;

    public GetMachineryOperatorsQueryHandler(ILogger<GetMachineryOperatorsQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<OfficerModel?>?>?>> Handle(GetMachineryOperatorsQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetMachineryOperators(request.Adapt<GetMachineryOperatorsRequest>(), ct);

            return (result?.Value?.Data?.Any() ?? false) ?
                new DataResult<List<OfficerModel?>?>
                {
                    Data = result.Value!.Data!,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<OfficerModel?>?>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<OfficerModel?>?>>(SharedErrors.UnknownError);
        }
    }
}