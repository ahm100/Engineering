using Engineering.Application.Abstractions.Data.CostOvers;
using Engineering.Application.Services.CostOvers.Models.GetCostOverByCode;
using Engineering.Domain.Entities.CostOvers;

namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverByCodeForResponse;

public class GetCostOverByCodeForResponseQueryHandler : IQueryHandler<GetCostOverByCodeForResponseQuery, GetCostOverByCodeResponse?>
{
    private readonly ILogger<GetCostOverByCodeForResponseQueryHandler> _logger;
    private readonly ICostOverRepository _repository;

    public GetCostOverByCodeForResponseQueryHandler(
        ILogger<GetCostOverByCodeForResponseQueryHandler> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCostOverByCodeResponse?>> Handle(
        GetCostOverByCodeForResponseQuery request, CT ct)
    {
        try
        {
            var CostOverResponse = await _repository.GetCostOverByCodeForResponse(
                request.CostOverCode,
                null, ct);
            return CostOverResponse ?? Result.Failure<GetCostOverByCodeResponse>(CostOverErrors.CostOverWithCodeNotFound)!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCostOverByCodeResponse>(SharedErrors.UnknownError)!;
        }
    }
}
