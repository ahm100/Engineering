using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.UpdateConsumableVolumes;

public class UpdateConsumableVolumesCommandHandler : ICommandHandler<UpdateConsumableVolumesCommand, bool>
{
    private readonly ILogger<UpdateConsumableVolumesCommand> _logger;
    private readonly IConsumableVolumeExpertRepository _expertRepository;
    private readonly IConsumableVolumeMachineryRepository _machineryRepository;
    private readonly IConsumableVolumeProductRepository _productRepository;

    public UpdateConsumableVolumesCommandHandler(
        ILogger<UpdateConsumableVolumesCommand> logger,
        IConsumableVolumeExpertRepository expertRepository,
        IConsumableVolumeMachineryRepository machineryRepository,
        IConsumableVolumeProductRepository productRepository)
    {
        _logger = logger;
        _expertRepository = expertRepository;
        _machineryRepository = machineryRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<bool>> Handle(UpdateConsumableVolumesCommand request, CT ct)
    {
        try
        {
            if (request.Experts is not null && request.Experts.Count > 0)
                foreach (var expert in request.Experts)
                    await _expertRepository.Update(expert);

            if (request.Machineries is not null && request.Machineries.Count > 0)
                foreach (var machinery in request.Machineries)
                    await _machineryRepository.Update(machinery);

            if (request.Products is not null && request.Products.Count > 0)
                foreach (var product in request.Products)
                    await _productRepository.Update(product);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}