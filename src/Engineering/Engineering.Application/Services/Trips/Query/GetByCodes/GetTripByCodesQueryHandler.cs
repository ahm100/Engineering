using Engineering.Application.Abstractions.Data.Trips;
using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Queries.GetByCodes;

public class GetTripByCodesQueryHandler : IQueryHandler<GetTripByCodesQuery, List<Trip>?>
{
    private readonly ILogger<GetTripByCodesQueryHandler> _logger;
    private readonly ITripRepository _repository;

    public GetTripByCodesQueryHandler(ILogger<GetTripByCodesQueryHandler> logger, ITripRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<Trip>?>> Handle(GetTripByCodesQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetByCodes(request.TripCodes, request.CompanyId, ct);

            return item ?? Result.Failure<List<Trip>?>(TripErrors.TripWithCodesNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<Trip>?>(SharedErrors.UnknownError);
        }
    }
}