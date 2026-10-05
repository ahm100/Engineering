using Engineering.Application.Abstractions.Data.FixAssetMachineries;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryDriverIds;

public class GetFixAssetMachineryDriverIdsQueryHandler : IQueryHandler<GetFixAssetMachineryDriverIdsQuery, List<long>?>
{
    private readonly IFixAssetMachineryRepository _repository;
    private readonly ILogger<GetFixAssetMachineryDriverIdsQueryHandler> _logger;

    public GetFixAssetMachineryDriverIdsQueryHandler(ILogger<GetFixAssetMachineryDriverIdsQueryHandler> logger, IFixAssetMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<long>?>> Handle(GetFixAssetMachineryDriverIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFixAssetMachineryDriverIds(ct);

            return result ?? Result.Failure<List<long>?>(SharedErrors.UnknownError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<long>?>(SharedErrors.UnknownError);
        }
    }
}