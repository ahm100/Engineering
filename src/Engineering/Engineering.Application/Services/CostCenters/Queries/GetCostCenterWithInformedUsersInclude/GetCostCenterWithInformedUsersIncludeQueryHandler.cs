using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithInformedUsersInclude;

public class GetCostCenterWithInformedUsersIncludeQueryHandler : IQueryHandler<GetCostCenterWithInformedUsersIncludeQuery, CostCenter>
{
    private readonly ILogger<GetCostCenterWithInformedUsersIncludeQueryHandler> _logger;
    private readonly ICostCenterRepository _repository;

    public GetCostCenterWithInformedUsersIncludeQueryHandler(ILogger<GetCostCenterWithInformedUsersIncludeQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenter?>> Handle(GetCostCenterWithInformedUsersIncludeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCostCenterWithInformedUsersInclude(request.Id, ct);

            return result ?? Result.Failure<CostCenter>(CostCenterErrors.CostCenterChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}
