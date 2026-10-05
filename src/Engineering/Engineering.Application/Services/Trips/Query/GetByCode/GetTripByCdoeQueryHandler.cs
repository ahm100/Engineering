using Engineering.Application.Abstractions.Data.Trips;
using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Queries.GetByCode;

public class GetTripByCodeQueryHandler : IQueryHandler<GetTripByCodeQuery, Trip?>
{
    private readonly ILogger<GetTripByCodeQueryHandler> _logger;
    private readonly ITripRepository _repository;

    public GetTripByCodeQueryHandler(ILogger<GetTripByCodeQueryHandler> logger, ITripRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Trip?>> Handle(GetTripByCodeQuery request, CT ct)
    {
        try
        {
            var item = await _repository.FindByCode(request.TripCode, request.CompanyId, ct);

            return item ?? Result.Failure<Trip?>(TripErrors.TripWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Trip?>(SharedErrors.UnknownError);
        }
    }
}