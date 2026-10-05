using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetsCostCenterTypeByIds;

public class GetsCostCenterTypeByIdsQueryHandler : IQueryHandler<GetsCostCenterTypeByIdsQuery, List<CostCenterType>>
{
    private readonly ICostCenterTypeRepository _repository;
    private readonly ILogger<GetsCostCenterTypeByIdsQuery> _logger;

    public GetsCostCenterTypeByIdsQueryHandler(
        ILogger<GetsCostCenterTypeByIdsQuery> logger,
        ICostCenterTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<CostCenterType>?>> Handle(GetsCostCenterTypeByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCostCenterTypeByIds(request.Items, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<CostCenterType>>(SharedErrors.UnknownError);
        }
    }
}