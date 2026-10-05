using Engineering.Application.Abstractions.Data.Trips;
using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Queries.GetsTripByIds;

public class GetsTripByIdsQueryHandler : IQueryHandler<GetsTripByIdsQuery, List<Trip>>
{
    private readonly ITripRepository _repository;
    private readonly ILogger<GetsTripByIdsQuery> _logger;

    public GetsTripByIdsQueryHandler(ILogger<GetsTripByIdsQuery> logger, ITripRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<Trip>?>> Handle(GetsTripByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsTripByIds(request.Items, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<Trip>>(SharedErrors.UnknownError);
        }
    }
}
