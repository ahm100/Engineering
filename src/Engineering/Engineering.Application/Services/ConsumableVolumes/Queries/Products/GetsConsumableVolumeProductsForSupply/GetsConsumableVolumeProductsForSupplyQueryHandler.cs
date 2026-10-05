using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetsConsumableVolumeProductsForSupply;

public class GetsConsumableVolumeProductsForSupplyQueryHandler : IQueryHandler<GetsConsumableVolumeProductsForSupplyQuery, DataResult<List<ConsumableVolumeProduct>>>
{
    private readonly ILogger<GetsConsumableVolumeProductsForSupplyQueryHandler> _logger;
    private readonly IConsumableVolumeProductRepository _repository;

    public GetsConsumableVolumeProductsForSupplyQueryHandler(
        ILogger<GetsConsumableVolumeProductsForSupplyQueryHandler> logger,
        IConsumableVolumeProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ConsumableVolumeProduct>>?>> Handle(GetsConsumableVolumeProductsForSupplyQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsConsumableVolumeProductsForSupply(request.Type, request.ProjectOperationId, request.ProjectOperationDetailId, request.ProductGroupId, request.ContractorId,
               request.OrderBy, request.PageIndex, request.PageSize, ct);

            var response = result.Data.Any() ? new DataResult<List<ConsumableVolumeProduct>>
            {
                Data = result.Data,
                RowCount = result.RowCount
            } : Result.Failure<DataResult<List<ConsumableVolumeProduct>>>(ConsumableVolumeProductErrors.NoHaveProducts);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<ConsumableVolumeProduct>>>(SharedErrors.UnknownError);
        }
    }
}
