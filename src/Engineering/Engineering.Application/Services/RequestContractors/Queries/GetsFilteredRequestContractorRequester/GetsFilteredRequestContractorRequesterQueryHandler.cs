using Engineering.Application.Abstractions.Data.RequestContractors;

namespace Engineering.Application.Services.RequestContractors.Queries.GetsFilteredRequestContractorRequester;

public class GetsFilteredRequestContractorRequesterQueryHandler : IQueryHandler<GetsFilteredRequestContractorRequesterQuery, DataResult<List<long>>>
{
    private readonly IRequestContractorRepository _repository;
    private readonly ILogger<GetsFilteredRequestContractorRequesterQueryHandler> _logger;

    public GetsFilteredRequestContractorRequesterQueryHandler(ILogger<GetsFilteredRequestContractorRequesterQueryHandler> logger, IRequestContractorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long>>?>> Handle(GetsFilteredRequestContractorRequesterQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredRequester(ct);

            return items.Data.Any() ?
               new DataResult<List<long>>
               {
                   Data = items.Data,
                   RowCount = items.RowCount
               } : Result.Failure<DataResult<List<long>>>(RequestContractorErrors.RequestContractorsNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<long>>>(SharedErrors.UnknownError);
        }
    }
}
