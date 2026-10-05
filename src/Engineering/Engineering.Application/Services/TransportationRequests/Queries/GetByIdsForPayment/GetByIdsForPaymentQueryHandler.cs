using Engineering.Application.Abstractions.Data.Transportations;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetByIdsForPayment;

public class GetByIdsForPaymentQueryHandler : IQueryHandler<GetByIdsForPaymentQuery, List<TransportationRequest>?>
{
    private readonly ILogger<GetByIdsForPaymentQueryHandler> _logger;
    private readonly ITransportationRequestRepository _repository;

    public GetByIdsForPaymentQueryHandler(ILogger<GetByIdsForPaymentQueryHandler> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<TransportationRequest>?>> Handle(GetByIdsForPaymentQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetByIdsForPayment(request.Ids, ct);

            return item ?? Result.Failure<List<TransportationRequest>?>(TransportationRequestErrors.TransportationRequestWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<TransportationRequest>?>(SharedErrors.UnknownError);
        }
    }
}