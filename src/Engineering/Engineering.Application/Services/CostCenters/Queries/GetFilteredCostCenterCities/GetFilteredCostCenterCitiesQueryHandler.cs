using Engineering.Application.Abstractions.Data.CostCenters;

namespace Engineering.Application.Services.CostCenters.Queries.GetFilteredCostCenterCities;

public class GetFilteredCostCenterCitiesQueryHandler : IQueryHandler<GetFilteredCostCenterCitiesQuery, List<long>>
{
    private readonly ICostCenterRepository _repository;
    private readonly ILogger<GetFilteredCostCenterCitiesQueryHandler> _logger;

    public GetFilteredCostCenterCitiesQueryHandler(ILogger<GetFilteredCostCenterCitiesQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<long>?>> Handle(GetFilteredCostCenterCitiesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredCostCenterCities(request.CostCenterIds, ct);

            return result != null && result.Count > 0 ? result : new List<long>(0);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<long>>(SharedErrors.UnknownError);
        }
    }
}