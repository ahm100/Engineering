using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetTotalSupplyByProjectOperationDetailIds;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetTotalSupplyByProjectOperationDetailIds;

public class GetTotalSupplyByProjectOperationDetailIdsQueryHandler : IQueryHandler<GetTotalSupplyByProjectOperationDetailIdsQuery, List<GetTotalSupplyByProjectOperationDetailIdsModel>>
{
    private readonly ILogger<GetTotalSupplyByProjectOperationDetailIdsQueryHandler> _logger;
    private readonly IConsumableVolumeProductRepository _repository;

    public GetTotalSupplyByProjectOperationDetailIdsQueryHandler(ILogger<GetTotalSupplyByProjectOperationDetailIdsQueryHandler> logger, IConsumableVolumeProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<GetTotalSupplyByProjectOperationDetailIdsModel>?>> Handle(GetTotalSupplyByProjectOperationDetailIdsQuery request, CT ct)
    {

        try
        {
            var result = await _repository.GetTotalSupplyByProjectOperationDetailIdsAsync(request.ProjectOperationDetailIds, request.ProductGroupId, ct);

            return result.Any() ? result : new List<GetTotalSupplyByProjectOperationDetailIdsModel>(0);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<GetTotalSupplyByProjectOperationDetailIdsModel>>(SharedErrors.UnknownError);
        }
    }
}
