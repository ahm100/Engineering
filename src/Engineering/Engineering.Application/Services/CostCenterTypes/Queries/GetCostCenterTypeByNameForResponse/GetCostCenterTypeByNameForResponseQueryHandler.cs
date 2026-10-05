using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByName;
using Polly.Caching;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByNameForResponse;

public class GetCostCenterTypeByNameForResponseQueryHandler : IQueryHandler<GetCostCenterTypeByNameForResponseQuery, GetCostCenterTypeByNameResponse?>
{
    private readonly ILogger<GetCostCenterTypeByNameForResponseQueryHandler> _logger;
    private readonly ICostCenterTypeRepository _repository;

    public GetCostCenterTypeByNameForResponseQueryHandler(
        ILogger<GetCostCenterTypeByNameForResponseQueryHandler> logger,
        ICostCenterTypeRepository repository
        )
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCostCenterTypeByNameResponse?>> Handle(
        GetCostCenterTypeByNameForResponseQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetCostCenterTypeByNameForResponse(
                request.CostCenterTypeName,
                null, ct);
            return entity ?? Result.Failure<GetCostCenterTypeByNameResponse?>(CostCenterErrors.CostCenterWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCostCenterTypeByNameResponse?>(SharedErrors.UnknownError);
        }
    }
}
