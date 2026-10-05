using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetProjectOperationDetailProducts;

public class GetProjectOperationDetailProductsQueryHandler : IQueryHandler<GetProjectOperationDetailProductsQuery, List<ConsumableVolumeProduct>>
{
    private readonly ILogger<GetProjectOperationDetailProductsQueryHandler> _logger;
    private readonly IConsumableVolumeProductRepository _repository;

    public GetProjectOperationDetailProductsQueryHandler(ILogger<GetProjectOperationDetailProductsQueryHandler> logger,
                                                         IConsumableVolumeProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<ConsumableVolumeProduct>?>> Handle(GetProjectOperationDetailProductsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailProducts(request.ProjectOperationDetailId, request.ProductGroupId, ct);

            return result.Any() ? result : Result.Failure<List<ConsumableVolumeProduct>>(ConsumableVolumeProductErrors.NoHaveProducts);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<ConsumableVolumeProduct>>(SharedErrors.UnknownError);
        }
    }
}
