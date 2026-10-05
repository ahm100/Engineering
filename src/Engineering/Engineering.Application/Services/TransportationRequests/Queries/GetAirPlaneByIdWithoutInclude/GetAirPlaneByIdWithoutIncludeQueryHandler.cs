using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Services.TransportationRequests.Models.GetAirplaneById;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetAirPlaneByIdWithoutInclude;

public class GetAirPlaneByIdWithoutIncludeQueryHandler : IQueryHandler<GetAirPlaneByIdWithoutIncludeQuery, GetAirplaneByIdResponse?>
{
    private readonly ILogger<GetAirPlaneByIdWithoutIncludeQueryHandler> _logger;
    private readonly ITransportationRequestRepository _repository;

    public GetAirPlaneByIdWithoutIncludeQueryHandler(ILogger<GetAirPlaneByIdWithoutIncludeQueryHandler> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetAirplaneByIdResponse?>> Handle(GetAirPlaneByIdWithoutIncludeQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetAirPlaneByIdWithoutInclude(request.Id, ct);

            return item ?? Result.Failure<GetAirplaneByIdResponse?>(TransportationRequestErrors.TransportationRequestWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetAirplaneByIdResponse?>(SharedErrors.UnknownError);
        }
    }
}