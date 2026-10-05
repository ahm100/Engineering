using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterByCodes;

public class GetCostCenterByCodesQueryHandler : IQueryHandler<GetCostCenterByCodesQuery, List<CostCenter>?>
{
    private readonly ILogger<GetCostCenterByCodesQueryHandler> _logger;
    private readonly ICostCenterRepository _repository;

    public GetCostCenterByCodesQueryHandler(ILogger<GetCostCenterByCodesQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<CostCenter>?>> Handle(GetCostCenterByCodesQuery request, CT ct)
    {
        try
        {
            var costCentersResponse = await _repository.GetByCodes(request.CostCenterCodes, request.CompanyId, ct);

            return costCentersResponse ?? Result.Failure<List<CostCenter>>(CostCenterErrors.CostCenterWithCodesNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<CostCenter>>(SharedErrors.UnknownError);
        }
    }
}