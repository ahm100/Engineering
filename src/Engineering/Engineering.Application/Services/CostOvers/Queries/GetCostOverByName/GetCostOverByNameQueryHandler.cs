using Engineering.Application.Abstractions.Data.CostOvers;
using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverByName;

public class GetCostOverByNameQueryHandler : IQueryHandler<GetCostOverByNameQuery, CostOver?>
{
    private readonly ILogger<GetCostOverByNameQueryHandler> _logger;
    private readonly ICostOverRepository _repository;

    public GetCostOverByNameQueryHandler(
        ILogger<GetCostOverByNameQueryHandler> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostOver?>> Handle(GetCostOverByNameQuery request, CT ct)
    {
        try
        {
            var CostOverResponse = await _repository.GetCostOverByName(
                request.CostOverName,
                request.CompanyId, ct);
            return CostOverResponse ?? Result.Failure<CostOver>(CostOverErrors.CostOverWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostOver>(SharedErrors.UnknownError);
        }
    }
}