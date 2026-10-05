using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterById;

public class GetCostCenterByIdQueryHandler : IQueryHandler<GetCostCenterByIdQuery, CostCenter>
{
    private readonly ILogger<GetCostCenterByIdQueryHandler> _logger;
    private readonly ICostCenterRepository _repository;

    public GetCostCenterByIdQueryHandler(ILogger<GetCostCenterByIdQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenter?>> Handle(GetCostCenterByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindByIdAndChild(request.Id, ct);

            return result ?? Result.Failure<CostCenter>(CostCenterErrors.CostCenterWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}