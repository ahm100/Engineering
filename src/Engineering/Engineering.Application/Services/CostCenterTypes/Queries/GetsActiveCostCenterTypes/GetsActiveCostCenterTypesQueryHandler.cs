using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Services.CostCenterTypes.Models.GetsActiveCostCenterTypes;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetsActiveCostCenterTypes;

public class GetsActiveCostCenterTypesQueryHandler : IQueryHandler<GetsActiveCostCenterTypesQuery, DataResult<List<GetsActiveCostCenterTypesModel>>>
{
    private readonly ICostCenterTypeRepository _repository;
    private readonly ILogger<GetsActiveCostCenterTypesQuery> _logger;

    public GetsActiveCostCenterTypesQueryHandler(
        ILogger<GetsActiveCostCenterTypesQuery> logger,
        ICostCenterTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsActiveCostCenterTypesModel>>?>> Handle(GetsActiveCostCenterTypesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsActiveCostCenterTypes(
                request.FilterData,
                request.Code,
                request.Name,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsActiveCostCenterTypesModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsActiveCostCenterTypesModel>>>(CostCenterTypeErrors.CostCenterTypesNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsActiveCostCenterTypesModel>>>(SharedErrors.UnknownError);
        }
    }
}