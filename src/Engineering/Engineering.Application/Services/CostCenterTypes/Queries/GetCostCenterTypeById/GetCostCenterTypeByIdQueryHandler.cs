using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterType = Engineering.Domain.Entities.CostCenters.CostCenterType;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeById;

public class GetCostCenterTypeByIdQueryHandler : IQueryHandler<GetCostCenterTypeByIdQuery, CostCenterType>
{
    private readonly ILogger<GetCostCenterTypeByIdQueryHandler> _logger;
    private readonly ICostCenterTypeRepository _repository;

    public GetCostCenterTypeByIdQueryHandler(
        ILogger<GetCostCenterTypeByIdQueryHandler> logger,
        ICostCenterTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterType?>> Handle(GetCostCenterTypeByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCostCenterTypeById(request.Id, ct);
            return result ?? Result.Failure<CostCenterType>(CostCenterTypeErrors.NotFoundWithId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterType>(SharedErrors.UnknownError);
        }
    }
}