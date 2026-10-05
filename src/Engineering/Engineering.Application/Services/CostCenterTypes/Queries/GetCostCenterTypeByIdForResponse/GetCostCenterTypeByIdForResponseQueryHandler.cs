using Aspose.Tasks;
using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeById;
namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByIdForResponse;

public class GetCostCenterTypeByIdForResponseQueryHandler : IQueryHandler<GetCostCenterTypeByIdForResponseQuery, GetCostCenterTypeByIdResponse?>
{
    private readonly ILogger<GetCostCenterTypeByIdForResponseQueryHandler> _logger;
    private readonly ICostCenterTypeRepository _repository;

    public GetCostCenterTypeByIdForResponseQueryHandler(
        ILogger<GetCostCenterTypeByIdForResponseQueryHandler> logger,
        ICostCenterTypeRepository repository
        )
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCostCenterTypeByIdResponse?>> Handle(
        GetCostCenterTypeByIdForResponseQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCostCenterTypeByIdForResponse(request.Id, ct);
            return result ?? Result.Failure<GetCostCenterTypeByIdResponse>(CostCenterTypeErrors.NotFoundWithId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCostCenterTypeByIdResponse>(SharedErrors.UnknownError);
        }
    }
}
