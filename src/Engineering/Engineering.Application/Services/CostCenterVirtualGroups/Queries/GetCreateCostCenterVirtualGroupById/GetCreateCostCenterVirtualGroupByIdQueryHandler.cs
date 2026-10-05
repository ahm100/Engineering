using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterVirtualGroups.Queries.GetCreateCostCenterVirtualGroupById;

public class GetCreateCostCenterVirtualGroupByIdQueryHandler : IQueryHandler<GetCreateCostCenterVirtualGroupByIdQuery, CostCenterVirtualGroup>
{
    private readonly ILogger<GetCreateCostCenterVirtualGroupByIdQueryHandler> _logger;
    private readonly ICostCenterVirtualGroupRepository _repository;

    public GetCreateCostCenterVirtualGroupByIdQueryHandler(ILogger<GetCreateCostCenterVirtualGroupByIdQueryHandler> logger,
                                                           ICostCenterVirtualGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterVirtualGroup?>> Handle(GetCreateCostCenterVirtualGroupByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindById(request.CostCenterVirtualGroupId, ct);

            return result ?? Result.Failure<CostCenterVirtualGroup>(CostCenterVirtualGroupErrors.CostCenterVirtualGroupNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterVirtualGroup>(SharedErrors.UnknownError);
        }
    }
}
