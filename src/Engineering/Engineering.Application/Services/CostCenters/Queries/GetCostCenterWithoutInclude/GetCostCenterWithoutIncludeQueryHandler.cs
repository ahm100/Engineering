using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithoutInclude;

public class GetCostCenterWithoutIncludeQueryHandler : IQueryHandler<GetCostCenterWithoutIncludeQuery, CostCenter>
{
    private readonly ILogger<GetCostCenterWithoutIncludeQueryHandler> _logger;
    private readonly ICostCenterRepository _repository;

    public GetCostCenterWithoutIncludeQueryHandler(ILogger<GetCostCenterWithoutIncludeQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenter?>> Handle(GetCostCenterWithoutIncludeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCostCenterWithoutInclude(request.Id, ct);

            return result ?? Result.Failure<CostCenter>(CostCenterErrors.CostCenterChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}