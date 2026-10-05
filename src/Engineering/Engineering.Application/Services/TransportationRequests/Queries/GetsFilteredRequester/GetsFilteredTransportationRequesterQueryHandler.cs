using Engineering.Application.Abstractions.Data.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetsFilteredRequester;

public class GetsFilteredTransportationRequesterQueryHandler : IQueryHandler<GetsFilteredTransportationRequesterQuery, DataResult<List<long>>>
{
    private readonly ITransportationRequestRepository _repository;
    private readonly ILogger<GetsFilteredTransportationRequesterQueryHandler> _logger;

    public GetsFilteredTransportationRequesterQueryHandler(ILogger<GetsFilteredTransportationRequesterQueryHandler> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long>>?>> Handle(GetsFilteredTransportationRequesterQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredRequester(ct);

            return items.Data.Any() ?
               new DataResult<List<long>>
               {
                   Data = items.Data,
                   RowCount = items.RowCount
               } : Result.Failure<DataResult<List<long>>>(TransportationRequestErrors.FilteredTransportationRequestNotFound);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return Result.Failure<DataResult<List<long>>>(SharedErrors.UnknownError);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
    }
}
