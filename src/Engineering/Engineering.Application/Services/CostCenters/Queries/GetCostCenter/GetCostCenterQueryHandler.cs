using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenter;

public class GetCostCenterQueryHandler : IQueryHandler<GetCostCenterQuery, CostCenter>
{
    private readonly ILogger<GetCostCenterQueryHandler> _logger;
    private readonly ICostCenterRepository _repository;

    public GetCostCenterQueryHandler(ILogger<GetCostCenterQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenter?>> Handle(GetCostCenterQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCostCenter(request.Id, ct);

            return result ?? Result.Failure<CostCenter>(CostCenterErrors.CostCenterWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}