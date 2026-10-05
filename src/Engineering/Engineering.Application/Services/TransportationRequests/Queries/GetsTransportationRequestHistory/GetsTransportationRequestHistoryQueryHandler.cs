using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestHistory;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetsTransportationRequestHistory;

public class GetsTransportationRequestHistoryQueryHandler : IQueryHandler<GetsTransportationRequestHistoryQuery, GetsTransportationRequestHistoryResponse>
{
    private readonly ITransportationRequestRepository _repository;
    private readonly ILogger<GetsTransportationRequestHistoryQueryHandler> _logger;

    public GetsTransportationRequestHistoryQueryHandler(ILogger<GetsTransportationRequestHistoryQueryHandler> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetsTransportationRequestHistoryResponse?>> Handle(GetsTransportationRequestHistoryQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetsTransportationRequestHistory(request.Id, ct);

            return item ?? Result.Failure<GetsTransportationRequestHistoryResponse>(TransportationRequestErrors.TransportationRequestWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetsTransportationRequestHistoryResponse>(SharedErrors.UnknownError);
        }
    }
}