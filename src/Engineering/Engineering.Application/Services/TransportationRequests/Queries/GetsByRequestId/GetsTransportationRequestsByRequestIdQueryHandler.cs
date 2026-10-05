using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Services.TransportationRequests.Queries.GetPaidByDate;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetByDate;

public class GetsTransportationRequestsByRequestIdQueryHandler : IQueryHandler<GetsTransportationRequestsByRequestIdQuery, List<TransportationRequest>>
{
    private readonly ILogger<GetsTransportationRequestsByRequestIdQueryHandler> _logger;
    private readonly ITransportationRequestRepository _repository;

    public GetsTransportationRequestsByRequestIdQueryHandler(ILogger<GetsTransportationRequestsByRequestIdQueryHandler> logger,
                                                             ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<TransportationRequest>?>> Handle(GetsTransportationRequestsByRequestIdQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsByRequestIdAsync(request.RequestById,
                                                               request.StartDate,
                                                               request.EndDate,
                                                               request.Status,
                                                               ct);

            return items.Any() ? items
                 : Result.Failure<List<TransportationRequest>>(TransportationRequestErrors.FilteredTransportationRequestNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<TransportationRequest>>(SharedErrors.UnknownError);
        }
    }
}
