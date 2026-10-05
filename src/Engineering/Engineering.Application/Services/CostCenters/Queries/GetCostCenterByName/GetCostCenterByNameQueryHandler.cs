using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterByName;

public class GetCostCenterByNameQueryHandler : IQueryHandler<GetCostCenterByNameQuery, CostCenter>
{
    private readonly ILogger<GetCostCenterByNameQueryHandler> _logger;
    private readonly ICostCenterRepository _repository;

    public GetCostCenterByNameQueryHandler(ILogger<GetCostCenterByNameQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenter?>> Handle(GetCostCenterByNameQuery request, CT ct)
    {
        try
        {
            var costCenterResponse = await _repository.FindByName(request.CostCenterName, request.CompanyId, ct);

            return costCenterResponse ?? Result.Failure<CostCenter>(CostCenterErrors.CostCenterWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}