using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachineryNotWork = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryNotWork;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryNotWorks;

public class GetFixAssetMachineryNotWorksQueryHandler : IQueryHandler<GetFixAssetMachineryNotWorksQuery, DataResult<List<FixAssetMachineryNotWork>>>
{
    private readonly IFixAssetMachineryNotWorkRepository _repository;
    private readonly ILogger<GetFixAssetMachineryNotWorksQueryHandler> _logger;

    public GetFixAssetMachineryNotWorksQueryHandler(ILogger<GetFixAssetMachineryNotWorksQueryHandler> logger, IFixAssetMachineryNotWorkRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<FixAssetMachineryNotWork>>?>> Handle(GetFixAssetMachineryNotWorksQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFixAssetMachineryNotWorks(request.FixAssetMachineryIds,
                request.MachineryIds, request.Type, request.FromDate, request.ToDate, request.FilterData, request.OrderBy,
                request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<FixAssetMachineryNotWork>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<FixAssetMachineryNotWork>>>(FixAssetMachineryErrors.FilteredFixAssetMachineryNotWorkNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<FixAssetMachineryNotWork>>>(SharedErrors.UnknownError);
        }
    }
}