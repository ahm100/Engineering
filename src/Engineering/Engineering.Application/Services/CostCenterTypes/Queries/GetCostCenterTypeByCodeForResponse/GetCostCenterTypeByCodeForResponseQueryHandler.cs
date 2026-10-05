using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByCode;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByCodeForResponse;

public class GetCostCenterTypeByCodeForResponseQueryHandler : IQueryHandler<GetCostCenterTypeByCodeForResponseQuery, GetCostCenterTypeByCodeResponse?>
{
    private readonly ILogger<GetCostCenterTypeByCodeForResponseQueryHandler> _logger;
    private readonly ICostCenterTypeRepository _repository;

    public GetCostCenterTypeByCodeForResponseQueryHandler(
        ILogger<GetCostCenterTypeByCodeForResponseQueryHandler> logger,
        ICostCenterTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCostCenterTypeByCodeResponse?>> Handle(
        GetCostCenterTypeByCodeForResponseQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetCostCenterTypeByCodeForResponse(
                request.CostCenterTypeCode,
                null, ct);
            
            return entity ?? Result.Failure<GetCostCenterTypeByCodeResponse>(CostCenterErrors.CostCenterWithCodeNotFound)!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCostCenterTypeByCodeResponse>(SharedErrors.UnknownError)!;
        }
    }
}
