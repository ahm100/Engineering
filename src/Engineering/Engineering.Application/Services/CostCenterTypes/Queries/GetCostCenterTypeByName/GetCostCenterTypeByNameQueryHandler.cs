using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByName;

public class GetCostCenterTypeByNameQueryHandler : IQueryHandler<GetCostCenterTypeByNameQuery, CostCenterType?>
{
    private readonly ILogger<GetCostCenterTypeByNameQueryHandler> _logger;
    private readonly ICostCenterTypeRepository _repository;

    public GetCostCenterTypeByNameQueryHandler(
        ILogger<GetCostCenterTypeByNameQueryHandler> logger,
        ICostCenterTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterType?>> Handle(GetCostCenterTypeByNameQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetCostCenterTypeByName(
                request.CostCenterTypeName,
                request.CompanyId, ct);
            return entity ?? Result.Failure<CostCenterType>(CostCenterErrors.CostCenterWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterType>(SharedErrors.UnknownError);
        }
    }
}