using Engineering.Application.Abstractions.Data.CostOvers;
using Engineering.Application.Services.CostOvers.Models.GetCostOverByName;
using Engineering.Application.Services.CostOvers.Queries.GetCostOverByName;
using Engineering.Domain.Entities.CostOvers;

namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverByNameForResponse;

public class GetCostOverByNameForResponseQueryHandler : IQueryHandler<GetCostOverByNameForResponseQuery, GetCostOverByNameResponse?>
{
    private readonly ILogger<GetCostOverByNameForResponseQueryHandler> _logger;
    private readonly ICostOverRepository _repository;

    public GetCostOverByNameForResponseQueryHandler(
        ILogger<GetCostOverByNameForResponseQueryHandler> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCostOverByNameResponse?>> Handle(
        GetCostOverByNameForResponseQuery request, CT ct)
    {
        try
        {
            var CostOverResponse = await _repository.GetCostOverByNameForResponse(
                request.CostName,
                null, ct);
            return CostOverResponse ?? Result.Failure<GetCostOverByNameResponse>(CostOverErrors.CostOverWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCostOverByNameResponse>(SharedErrors.UnknownError);
        }
    }
}
