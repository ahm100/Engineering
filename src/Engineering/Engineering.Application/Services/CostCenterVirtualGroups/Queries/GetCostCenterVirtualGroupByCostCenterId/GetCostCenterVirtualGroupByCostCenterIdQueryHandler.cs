using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterVirtualGroups.Queries.CostCenterVirtualGroupById;

public class GetCostCenterVirtualGroupByCostCenterIdQueryHandler : IQueryHandler<GetCostCenterVirtualGroupByCostCenterIdQuery, DataResult<List<CostCenterVirtualGroup>>>
{
    private readonly ILogger<GetCostCenterVirtualGroupByCostCenterIdQueryHandler> _logger;
    private readonly ICostCenterVirtualGroupRepository _repository;

    public GetCostCenterVirtualGroupByCostCenterIdQueryHandler(ILogger<GetCostCenterVirtualGroupByCostCenterIdQueryHandler> logger,
                                                               ICostCenterVirtualGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<CostCenterVirtualGroup>>?>> Handle(GetCostCenterVirtualGroupByCostCenterIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByCostCenterAsync(request.CostCenterId, request.PageIndex, request.PageSize, ct);
            return result.Data.Any() ?
                new DataResult<List<CostCenterVirtualGroup>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<CostCenterVirtualGroup>>>(CostCenterVirtualGroupErrors.CostCenterVirtualGroupNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<CostCenterVirtualGroup>>>(SharedErrors.UnknownError);
        }
    }
}
