
using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterAuthorizedUser = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedUser;

namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Queries.AuthorizedUserGetsByCostCenterId;

public class AuthorizedUserGetsByCostCenterIdQueryHandler : IQueryHandler<AuthorizedUserGetsByCostCenterIdQuery, DataResult<List<CostCenterAuthorizedUser>>>
{
    private readonly ICostCenterAuthorizedUserRepository _repository;
    private readonly ILogger<AuthorizedUserGetsByCostCenterIdQueryHandler> _logger;

    public AuthorizedUserGetsByCostCenterIdQueryHandler(ILogger<AuthorizedUserGetsByCostCenterIdQueryHandler> logger, ICostCenterAuthorizedUserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<CostCenterAuthorizedUser>>?>> Handle(AuthorizedUserGetsByCostCenterIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetAuthorizedUsersByCostCenter(request.CostCenterId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<CostCenterAuthorizedUser>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<CostCenterAuthorizedUser>>>(CostCenterAuthorizedUserErrors.DataIsNull);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<CostCenterAuthorizedUser>>>(SharedErrors.UnknownError);
        }
    }
}