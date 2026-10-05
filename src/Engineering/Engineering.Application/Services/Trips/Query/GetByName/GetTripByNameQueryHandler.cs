using Engineering.Application.Abstractions.Data.Trips;
using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Queries.GetByName;

public class GetTripByNameQueryHandler : IQueryHandler<GetTripByNameQuery, Trip?>
{
    private readonly ILogger<GetTripByNameQueryHandler> _logger;
    private readonly ITripRepository _repository;

    public GetTripByNameQueryHandler(ILogger<GetTripByNameQueryHandler> logger, ITripRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Trip?>> Handle(GetTripByNameQuery request, CT ct)
    {
        try
        {
            var item = await _repository.FindByName(request.TripName, request.CompanyId, ct);

            return item ?? Result.Failure<Trip?>(TripErrors.TripWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Trip?>(SharedErrors.UnknownError);
        }
    }
}
