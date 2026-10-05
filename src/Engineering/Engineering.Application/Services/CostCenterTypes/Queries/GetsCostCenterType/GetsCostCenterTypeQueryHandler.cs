using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterType;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetsCostCenterType;

public class GetsCostCenterTypeQueryHandler : IQueryHandler<GetsCostCenterTypeQuery, DataResult<List<GetsCostCenterTypeModel>>>
{
    private readonly ICostCenterTypeRepository _repository;
    private readonly ILogger<GetsCostCenterTypeQueryHandler> _logger;

    public GetsCostCenterTypeQueryHandler(
        ILogger<GetsCostCenterTypeQueryHandler> logger,
        ICostCenterTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsCostCenterTypeModel>>?>> Handle(GetsCostCenterTypeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCostCenterType(
                request.Ids,
                request.FilterData,
                request.IsActive,
                request.OrderBy,
                request.CompanyId,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsCostCenterTypeModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsCostCenterTypeModel>>>(CostCenterTypeErrors.CostCenterTypesNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsCostCenterTypeModel>>>(SharedErrors.UnknownError);
        }
    }
}