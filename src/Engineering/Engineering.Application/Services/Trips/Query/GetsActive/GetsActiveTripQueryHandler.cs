using Engineering.Application.Abstractions.Data.Trips;
using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Queries.GetsActive;

public class GetsActiveTripQueryHandler : IQueryHandler<GetsActiveTripQuery, DataResult<List<Trip>>>
{
    private readonly ITripRepository _repository;
    private readonly ILogger<GetsActiveTripQuery> _logger;

    public GetsActiveTripQueryHandler(ILogger<GetsActiveTripQuery> logger, ITripRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Trip>>?>> Handle(GetsActiveTripQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsActiveTrip(request.FilterData, request.TripCode, request.TripName, request.CompanyId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<Trip>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<Trip>>>(TripErrors.FilteredTripNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Trip>>>(SharedErrors.UnknownError);
        }
    }
}