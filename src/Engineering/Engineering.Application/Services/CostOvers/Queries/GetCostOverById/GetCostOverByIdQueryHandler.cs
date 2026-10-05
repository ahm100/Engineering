using Engineering.Application.Abstractions.Data.CostOvers;
using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverById;

public class GetCostOverByIdQueryHandler : IQueryHandler<GetCostOverByIdQuery, CostOver?>
{
    private readonly ILogger<GetCostOverByIdQueryHandler> _logger;
    private readonly ICostOverRepository _repository;

    public GetCostOverByIdQueryHandler(
        ILogger<GetCostOverByIdQueryHandler> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostOver?>> Handle(GetCostOverByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCostOverById(request.Id, ct);
            return result ?? Result.Failure<CostOver?>(CostOverErrors.CostOverWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostOver?>(SharedErrors.UnknownError);
        }
    }
}