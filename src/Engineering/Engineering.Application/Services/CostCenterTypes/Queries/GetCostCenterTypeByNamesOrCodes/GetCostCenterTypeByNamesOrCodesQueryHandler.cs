using Engineering.Application.Abstractions.Data.CostCenters;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByNamesOrCodes;

public class GetCostCenterTypeByNamesOrCodesQueryHandler : IQueryHandler<GetCostCenterTypeByNamesOrCodesQuery, bool>
{
    private readonly ILogger<GetCostCenterTypeByNamesOrCodesQueryHandler> _logger;
    private readonly ICostCenterTypeRepository _repository;

    public GetCostCenterTypeByNamesOrCodesQueryHandler(
        ILogger<GetCostCenterTypeByNamesOrCodesQueryHandler> logger,
        ICostCenterTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(GetCostCenterTypeByNamesOrCodesQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetCostCenterTypeByNamesOrCodes(
                request.Names,
                request.Codes,
                request.CompanyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}