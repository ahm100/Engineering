using Engineering.Application.Abstractions.Data.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetsFilteredMachineryRequester;

public class GetsFilteredMachineryRequesterQueryHandler : IQueryHandler<GetsFilteredMachineryRequesterQuery, DataResult<List<long>>>
{
    private readonly IRequestMachineryRepository _repository;
    private readonly ILogger<GetsFilteredMachineryRequesterQueryHandler> _logger;

    public GetsFilteredMachineryRequesterQueryHandler(ILogger<GetsFilteredMachineryRequesterQueryHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long>>?>> Handle(GetsFilteredMachineryRequesterQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredRequester(ct);

            return items.Data.Any() ?
               new DataResult<List<long>>
               {
                   Data = items.Data,
                   RowCount = items.RowCount
               } : Result.Failure<DataResult<List<long>>>(RequestMachineryErrors.FilteredMachineryRequestNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<long>>>(SharedErrors.UnknownError);
        }
    }
}
