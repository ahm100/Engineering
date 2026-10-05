using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachineryRate = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryRate;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetsRateByFixAssetMachineryId;

public class GetsRateByFixAssetMachineryIdQueryHandler : IQueryHandler<GetsRateByFixAssetMachineryIdQuery, DataResult<List<FixAssetMachineryRate?>>>
{
    private readonly IFixAssetMachineryRateRepository _repository;
    private readonly ILogger<GetsRateByFixAssetMachineryIdQueryHandler> _logger;

    public GetsRateByFixAssetMachineryIdQueryHandler(ILogger<GetsRateByFixAssetMachineryIdQueryHandler> logger, IFixAssetMachineryRateRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<FixAssetMachineryRate?>>?>> Handle(GetsRateByFixAssetMachineryIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsRateByFixAssetMachineryId(
                request.FixAssetMachineryId,
                request.StartDate,
                request.EndDate,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<FixAssetMachineryRate?>>
                {
                    Data = result.Data!,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<FixAssetMachineryRate?>>>(FixAssetMachineryErrors.FilteredRateNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<FixAssetMachineryRate?>>>(SharedErrors.UnknownError);
        }
    }
}