using Engineering.Application.Abstractions.Data.CostOvers;
using Engineering.Application.Services.CostOvers.Models.GetCostOverById;
using Engineering.Application.Services.CostOvers.Queries.GetCostOverById;

namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverByIdForResponse;

public class GetCostOverByIdForResponseQueryHandler : IQueryHandler<GetCostOverByIdForResponseQuery, GetCostOverByIdResponse?>
{
    private readonly ILogger<GetCostOverByIdForResponseQueryHandler> _logger;
    private readonly ICostOverRepository _repository;
    public GetCostOverByIdForResponseQueryHandler(
        ILogger<GetCostOverByIdForResponseQueryHandler> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCostOverByIdResponse?>> Handle(
        GetCostOverByIdForResponseQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCostOverByIdForResponse(request.Id, ct);
            return result ?? Result.Failure<GetCostOverByIdResponse?>(CostOverErrors.CostOverWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCostOverByIdResponse?>(SharedErrors.UnknownError);
        }
    }
}