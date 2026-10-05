using Engineering.Application.Abstractions.Data.Trips;
using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Queries.GetById;

public class GetTripByIdQueryHandler : IQueryHandler<GetTripByIdQuery, Trip?>
{
    private readonly ILogger<GetTripByIdQueryHandler> _logger;
    private readonly ITripRepository _repository;

    public GetTripByIdQueryHandler(ILogger<GetTripByIdQueryHandler> logger, ITripRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Trip?>> Handle(GetTripByIdQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetById(request.Id, ct);

            return item ?? Result.Failure<Trip?>(TripErrors.TripWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Trip?>(SharedErrors.UnknownError);
        }
    }
}