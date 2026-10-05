using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithRolesAndUsersInclude;

public class GetCostCenterWithRolesAndUsersIncludeQueryHandler : IQueryHandler<GetCostCenterWithRolesAndUsersIncludeQuery, CostCenter>
{
    private readonly ILogger<GetCostCenterWithRolesAndUsersIncludeQueryHandler> _logger;
    private readonly ICostCenterRepository _repository;

    public GetCostCenterWithRolesAndUsersIncludeQueryHandler(ILogger<GetCostCenterWithRolesAndUsersIncludeQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenter?>> Handle(GetCostCenterWithRolesAndUsersIncludeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCostCenterWithRolesAndUsersInclude(request.Id, ct);

            return result ?? Result.Failure<CostCenter>(CostCenterErrors.CostCenterChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}
