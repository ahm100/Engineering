using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByCode;

public class GetCostCenterTypeByCodeQueryHandler : IQueryHandler<GetCostCenterTypeByCodeQuery, CostCenterType?>
{
    private readonly ILogger<GetCostCenterTypeByCodeQueryHandler> _logger;
    private readonly ICostCenterTypeRepository _repository;

    public GetCostCenterTypeByCodeQueryHandler(
        ILogger<GetCostCenterTypeByCodeQueryHandler> logger,
        ICostCenterTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterType?>> Handle(GetCostCenterTypeByCodeQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetCostCenterTypeByCode(
                request.CostCenterTypeCode,
                request.CompanyId, ct);

            return entity ?? Result.Failure<CostCenterType>(CostCenterErrors.CostCenterWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterType>(SharedErrors.UnknownError);
        }
    }
}