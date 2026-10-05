using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeProduct = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeProduct;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetProductsByProjectOperationDetailId;

public class GetProductsByProjectOperationDetailIdQueryHandler : IQueryHandler<GetProductsByProjectOperationDetailIdQuery, DataResult<List<ConsumableVolumeProduct>>>
{
    private readonly IConsumableVolumeProductRepository _repository;
    private readonly ILogger<GetProductsByProjectOperationDetailIdQueryHandler> _logger;

    public GetProductsByProjectOperationDetailIdQueryHandler(ILogger<GetProductsByProjectOperationDetailIdQueryHandler> logger, IConsumableVolumeProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ConsumableVolumeProduct>>?>> Handle(GetProductsByProjectOperationDetailIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByProjectOperationDetailId(request.ProjectOperationDetailId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ConsumableVolumeProduct>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ConsumableVolumeProduct>>>(ConsumableVolumeProductErrors.ProjectOperationDetailProductWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ConsumableVolumeProduct>>>(SharedErrors.UnknownError);
        }
    }
}
