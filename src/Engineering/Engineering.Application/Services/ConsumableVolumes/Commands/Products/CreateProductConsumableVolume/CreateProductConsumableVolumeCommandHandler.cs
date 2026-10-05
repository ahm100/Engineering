using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeProduct = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeProduct;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Products.CreateProductConsumableVolume;

public class CreateConsumableVolumeProductCommandHandler : ICommandHandler<CreateConsumableVolumeProductCommand, ConsumableVolumeProduct>
{
    private readonly ILogger<CreateConsumableVolumeProductCommand> _logger;
    private readonly IConsumableVolumeProductRepository _repository;

    public CreateConsumableVolumeProductCommandHandler(ILogger<CreateConsumableVolumeProductCommand> logger, IConsumableVolumeProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumableVolumeProduct?>> Handle(CreateConsumableVolumeProductCommand request, CT ct)
    {
        try
        {
            var entity = new ConsumableVolumeProduct(request.ProjectOperationDetail, request.ProductGroupId, request.UnusedPercentage,
                request.IsStandard, request.StandardValue, request.FinalValue, request.VolumeProductType);

            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsumableVolumeProduct>(SharedErrors.UnknownError);
        }
    }
}
