using Engineering.Application.Abstractions.Data.Transportations;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetAirplaneById;

public class GetAirplaneByIdQueryHandler : IQueryHandler<GetAirplaneByIdQuery, TransportationRequest?>
{
    private readonly ILogger<GetAirplaneByIdQueryHandler> _logger;
    private readonly ITransportationRequestRepository _repository;

    public GetAirplaneByIdQueryHandler(ILogger<GetAirplaneByIdQueryHandler> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(GetAirplaneByIdQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetById(request.Id, ct);

            return item ?? Result.Failure<TransportationRequest?>(TransportationRequestErrors.TransportationRequestWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationRequest?>(SharedErrors.UnknownError);
        }
    }
}