using Engineering.Application.Abstractions.Data.Transportations;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetByIdForPayment;

public class GetTransportationRequestByIdForPaymentQueryHandler : IQueryHandler<GetTransportationRequestByIdForPaymentQuery, TransportationRequest?>
{
    private readonly ILogger<GetTransportationRequestByIdForPaymentQueryHandler> _logger;
    private readonly ITransportationRequestRepository _repository;

    public GetTransportationRequestByIdForPaymentQueryHandler(ILogger<GetTransportationRequestByIdForPaymentQueryHandler> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(GetTransportationRequestByIdForPaymentQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetByIdForPayment(request.Id, ct);

            return item ?? Result.Failure<TransportationRequest?>(TransportationRequestErrors.TransportationRequestWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationRequest?>(SharedErrors.UnknownError);
        }
    }
}