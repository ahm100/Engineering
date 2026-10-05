using Engineering.Application.Abstractions.Data.CostOvers;
using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverByCode;

public class GetCostOverByCodeQueryHandler : IQueryHandler<GetCostOverByCodeQuery, CostOver>
{
    private readonly ILogger<GetCostOverByCodeQueryHandler> _logger;
    private readonly ICostOverRepository _repository;

    public GetCostOverByCodeQueryHandler(
        ILogger<GetCostOverByCodeQueryHandler> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostOver?>> Handle(GetCostOverByCodeQuery request, CT ct)
    {
        try
        {
            var CostOverResponse = await _repository.GetCostOverByCode(
                request.CostOverCode,
                request.CompanyId, ct);
            return CostOverResponse ?? Result.Failure<CostOver>(CostOverErrors.CostOverWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostOver>(SharedErrors.UnknownError);
        }
    }
}