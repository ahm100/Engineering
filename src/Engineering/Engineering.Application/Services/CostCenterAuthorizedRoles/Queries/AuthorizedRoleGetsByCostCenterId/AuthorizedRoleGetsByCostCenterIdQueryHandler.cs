using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterAuthorizedRole = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedRole;

namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Queries.AuthorizedRoleGetsByCostCenterId;

public class AuthorizedRoleGetsByCostCenterIdQueryHandler : IQueryHandler<AuthorizedRoleGetsByCostCenterIdQuery, DataResult<List<CostCenterAuthorizedRole>>>
{
    private readonly ICostCenterAuthorizedRoleRepository _repository;
    private readonly ILogger<AuthorizedRoleGetsByCostCenterIdQueryHandler> _logger;

    public AuthorizedRoleGetsByCostCenterIdQueryHandler(ILogger<AuthorizedRoleGetsByCostCenterIdQueryHandler> logger, ICostCenterAuthorizedRoleRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<CostCenterAuthorizedRole>>?>> Handle(AuthorizedRoleGetsByCostCenterIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetAuthorizedRolesByCostCenter(request.CostCenterId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<CostCenterAuthorizedRole>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<CostCenterAuthorizedRole>>>(CostCenterAuthorizedRoleErrors.DataIsNull);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<CostCenterAuthorizedRole>>>(SharedErrors.UnknownError);
        }
    }
}