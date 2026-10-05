
using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterInformedUser = Engineering.Domain.Entities.CostCenters.CostCenterInformedUser;

namespace Engineering.Application.Services.CostCenterInformedUsers.Queries.GetsByCostCenterId;

public class GetsByCostCenterIdQueryHandler : IQueryHandler<GetsByCostCenterIdQuery, DataResult<List<CostCenterInformedUser>>>
{
    private readonly ICostCenterInformedUserRepository _repository;
    private readonly ILogger<GetsByCostCenterIdQueryHandler> _logger;

    public GetsByCostCenterIdQueryHandler(ILogger<GetsByCostCenterIdQueryHandler> logger, ICostCenterInformedUserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<CostCenterInformedUser>>?>> Handle(GetsByCostCenterIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetInformedUsersByCostCenter(request.CostCenterId, ct);

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