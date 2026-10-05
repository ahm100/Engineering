using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterByIdIncludeless;

public class GetCostCenterByIdIncludelessQueryHandler : IQueryHandler<GetCostCenterByIdIncludelessQuery, CostCenter>
{
    private readonly ILogger<GetCostCenterByIdIncludelessQueryHandler> _logger;
    private readonly ICostCenterRepository _repository;

    public GetCostCenterByIdIncludelessQueryHandler(ILogger<GetCostCenterByIdIncludelessQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenter?>> Handle(GetCostCenterByIdIncludelessQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindById(request.Id, ct);

            return result ?? Result.Failure<CostCenter>(CostCenterErrors.CostCenterWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}