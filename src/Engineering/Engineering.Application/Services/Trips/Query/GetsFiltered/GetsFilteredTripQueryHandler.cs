using Engineering.Application.Abstractions.Data.Trips;
using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Queries.GetsFiltered;

public class GetsFilteredTripQueryHandler : IQueryHandler<GetsFilteredTripQuery, DataResult<List<Trip>>>
{
    private readonly ITripRepository _repository;
    private readonly ILogger<GetsFilteredTripQueryHandler> _logger;

    public GetsFilteredTripQueryHandler(ILogger<GetsFilteredTripQueryHandler> logger, ITripRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Trip>>?>> Handle(GetsFilteredTripQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredTrip(request.Ids, request.FilterData, request.TripCode, request.TripName, request.IsActive, request.CompanyId,
               request.OrderBy, request.PageIndex, request.PageSize, ct);

            return items.Data.Any() ?
                new DataResult<List<Trip>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<Trip>>>(TripErrors.FilteredTripNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Trip>>>(SharedErrors.UnknownError);
        }
    }
}