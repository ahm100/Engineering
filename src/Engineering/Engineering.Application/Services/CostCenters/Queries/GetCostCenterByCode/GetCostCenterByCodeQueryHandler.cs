using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterByCode;

public class GetCostCenterByCodeQueryHandler : IQueryHandler<GetCostCenterByCodeQuery, CostCenter>
{
    private readonly ILogger<GetCostCenterByCodeQueryHandler> _logger;
    private readonly ICostCenterRepository _repository;

    public GetCostCenterByCodeQueryHandler(ILogger<GetCostCenterByCodeQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenter?>> Handle(GetCostCenterByCodeQuery request, CT ct)
    {
        try
        {
            var costCenterResponse = await _repository.FindByCode(request.CostCenterCode, request.CompanyId, ct);

            return costCenterResponse ?? Result.Failure<CostCenter>(CostCenterErrors.CostCenterWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}