using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithWarehousesInclude;

public class GetCostCenterWithWarehousesIncludeQueryHandler : IQueryHandler<GetCostCenterWithWarehousesIncludeQuery, CostCenter>
{
    private readonly ILogger<GetCostCenterWithWarehousesIncludeQueryHandler> _logger;
    private readonly ICostCenterRepository _repository;

    public GetCostCenterWithWarehousesIncludeQueryHandler(ILogger<GetCostCenterWithWarehousesIncludeQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenter?>> Handle(GetCostCenterWithWarehousesIncludeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCostCenterWithWarehousesInclude(request.Id, ct);

            return result ?? Result.Failure<CostCenter>(CostCenterErrors.CostCenterChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}
