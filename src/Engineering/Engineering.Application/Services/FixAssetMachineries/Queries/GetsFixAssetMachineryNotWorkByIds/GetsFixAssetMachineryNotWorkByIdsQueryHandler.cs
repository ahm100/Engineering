using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachineryNotWork = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryNotWork;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetsFixAssetMachineryNotWorkByIds;

public class GetsFixAssetMachineryNotWorkByIdsQueryHandler : IQueryHandler<GetsFixAssetMachineryNotWorkByIdsQuery, DataResult<List<FixAssetMachineryNotWork?>>>
{
    private readonly IFixAssetMachineryNotWorkRepository _repository;
    private readonly ILogger<GetsFixAssetMachineryNotWorkByIdsQueryHandler> _logger;

    public GetsFixAssetMachineryNotWorkByIdsQueryHandler(ILogger<GetsFixAssetMachineryNotWorkByIdsQueryHandler> logger, IFixAssetMachineryNotWorkRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<FixAssetMachineryNotWork?>>?>> Handle(GetsFixAssetMachineryNotWorkByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsFixAssetMachineryNotWorkByIds(request.ids, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<FixAssetMachineryNotWork?>>
                {
                    Data = result.Data!,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<FixAssetMachineryNotWork?>>>(FixAssetMachineryErrors.FilteredFixAssetMachineryNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<FixAssetMachineryNotWork?>>>(SharedErrors.UnknownError);
        }
    }
}