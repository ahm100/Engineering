using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithUsersInclude;

public class GetCostCenterWithUsersIncludeQueryHandler : IQueryHandler<GetCostCenterWithUsersIncludeQuery, CostCenter>
{
    private readonly ILogger<GetCostCenterWithUsersIncludeQueryHandler> _logger;
    private readonly ICostCenterRepository _repository;

    public GetCostCenterWithUsersIncludeQueryHandler(ILogger<GetCostCenterWithUsersIncludeQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenter?>> Handle(GetCostCenterWithUsersIncludeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCostCenterWithUsersInclude(request.Id, ct);

            return result ?? Result.Failure<CostCenter>(CostCenterErrors.CostCenterChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}