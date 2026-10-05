using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetsByEmployerId;

public class GetsByEmployerIdQueryHandler : IQueryHandler<GetsByEmployerIdQuery, DataResult<List<CostCenter>>>
{
    private readonly ICostCenterRepository _repository;
    private readonly ILogger<GetsByEmployerIdQueryHandler> _logger;

    public GetsByEmployerIdQueryHandler(ILogger<GetsByEmployerIdQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<CostCenter>>?>> Handle(GetsByEmployerIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByEmployerId(request.FilterData, request.EmployerId, request.companyId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<CostCenter>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<CostCenter>>>(CostCenterErrors.FilteredCostCenterNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<CostCenter>>>(SharedErrors.UnknownError);
        }
    }
}