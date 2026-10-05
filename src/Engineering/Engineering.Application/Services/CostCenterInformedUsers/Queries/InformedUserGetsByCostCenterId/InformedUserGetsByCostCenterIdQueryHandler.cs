
using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterInformedUser = Engineering.Domain.Entities.CostCenters.CostCenterInformedUser;

namespace Engineering.Application.Services.CostCenterInformedUsers.Queries.InformedUserGetsByCostCenterId;

public class InformedUserGetsByCostCenterIdQueryHandler : IQueryHandler<InformedUserGetsByCostCenterIdQuery, DataResult<List<CostCenterInformedUser>>>
{
    private readonly ICostCenterInformedUserRepository _repository;
    private readonly ILogger<InformedUserGetsByCostCenterIdQueryHandler> _logger;

    public InformedUserGetsByCostCenterIdQueryHandler(ILogger<InformedUserGetsByCostCenterIdQueryHandler> logger, ICostCenterInformedUserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<CostCenterInformedUser>>?>> Handle(InformedUserGetsByCostCenterIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCostCenterInformedUserByCostCenter(request.CostCenterId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<CostCenterInformedUser>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<CostCenterInformedUser>>>(CostCenterInformedUserErrors.DataIsNull);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<CostCenterInformedUser>>>(SharedErrors.UnknownError);
        }
    }
}