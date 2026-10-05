using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeProduct = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeProduct;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetProductConsumableVolumeById;

public class GetConsumableVolumeProductByIdQueryHandler : IQueryHandler<GetConsumableVolumeProductByIdQuery, ConsumableVolumeProduct>
{
    private readonly ILogger<GetConsumableVolumeProductByIdQueryHandler> _logger;
    private readonly IConsumableVolumeProductRepository _repository;

    public GetConsumableVolumeProductByIdQueryHandler(ILogger<GetConsumableVolumeProductByIdQueryHandler> logger, IConsumableVolumeProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumableVolumeProduct?>> Handle(GetConsumableVolumeProductByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, ct);
            return result ?? Result.Failure<ConsumableVolumeProduct>(ConsumableVolumeProductErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsumableVolumeProduct>(SharedErrors.UnknownError);
        }
    }
}
