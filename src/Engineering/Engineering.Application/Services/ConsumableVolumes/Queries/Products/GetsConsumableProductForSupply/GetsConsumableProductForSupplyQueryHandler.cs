using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeProduct = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeProduct;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetsConsumableProductForSupply;

public class GetsConsumableProductForSupplyQueryHandler : IQueryHandler<GetsConsumableProductForSupplyQuery, DataResult<List<ConsumableVolumeProduct>>>
{
    private readonly IConsumableVolumeProductRepository _repository;
    private readonly ILogger<GetsConsumableProductForSupplyQueryHandler> _logger;

    public GetsConsumableProductForSupplyQueryHandler(ILogger<GetsConsumableProductForSupplyQueryHandler> logger, IConsumableVolumeProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ConsumableVolumeProduct>>?>> Handle(GetsConsumableProductForSupplyQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsConsumableProductForSupply(request.Type, request.ProjectOperationId, request.ProjectOperationDetailId, request.ProductGroupId, request.ContractorId,
               request.OrderBy, request.PageIndex, request.PageSize, ct);

            var response = result.Data.Any() ? new DataResult<List<ConsumableVolumeProduct>>
            {
                Data = result.Data,
                RowCount = result.RowCount
            } : Result.Failure<DataResult<List<ConsumableVolumeProduct>>>(ConsumableVolumeProductErrors.NoHaveProducts);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ConsumableVolumeProduct>>>(SharedErrors.UnknownError);
        }
    }
}
