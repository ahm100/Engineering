using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithRolesInclude;

public class GetCostCenterWithRolesIncludeQueryHandler : IQueryHandler<GetCostCenterWithRolesIncludeQuery, CostCenter>
{
    private readonly ILogger<GetCostCenterWithRolesIncludeQueryHandler> _logger;
    private readonly ICostCenterRepository _repository;

    public GetCostCenterWithRolesIncludeQueryHandler(ILogger<GetCostCenterWithRolesIncludeQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenter?>> Handle(GetCostCenterWithRolesIncludeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCostCenterWithRolesInclude(request.Id, ct);

            return result ?? Result.Failure<CostCenter>(CostCenterErrors.CostCenterChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}
