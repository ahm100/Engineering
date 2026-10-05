using Engineering.Application.Abstractions.Data.Transportations;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetSnapById;

public class GetSnapByIdQueryHandler : IQueryHandler<GetSnapByIdQuery, TransportationRequest?>
{
    private readonly ILogger<GetSnapByIdQueryHandler> _logger;
    private readonly ITransportationRequestRepository _repository;

    public GetSnapByIdQueryHandler(ILogger<GetSnapByIdQueryHandler> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(GetSnapByIdQuery request, CT ct)
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