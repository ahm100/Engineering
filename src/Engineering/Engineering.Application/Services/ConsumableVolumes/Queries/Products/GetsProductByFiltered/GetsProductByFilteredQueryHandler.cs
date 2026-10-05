using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeProduct = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeProduct;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetsProductsByFiltered;

public class GetsProductsByFilteredQueryHandler : IQueryHandler<GetsProductsByFilteredQuery, DataResult<List<ConsumableVolumeProduct>>>
{
    private readonly IConsumableVolumeProductRepository _repository;
    private readonly ILogger<GetsProductsByFilteredQueryHandler> _logger;

    public GetsProductsByFilteredQueryHandler(ILogger<GetsProductsByFilteredQueryHandler> logger, IConsumableVolumeProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ConsumableVolumeProduct>>?>> Handle(GetsProductsByFilteredQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProductsByFiltered(request.CostCenterId, request.ProjectId, request.ProjectOperationIds, request.ProjectOperationDetailIds, ct);

            return result.Any() ?
                new DataResult<List<ConsumableVolumeProduct>>
                {
                    Data = result,
                } : Result.Failure<DataResult<List<ConsumableVolumeProduct>>>(ProjectOperationErrors.ProjectChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ConsumableVolumeProduct>>>(SharedErrors.UnknownError);
        }
    }
}
