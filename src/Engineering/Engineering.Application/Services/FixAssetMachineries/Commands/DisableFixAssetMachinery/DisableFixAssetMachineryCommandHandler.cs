using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.DisableFixAssetMachinery;

public class DisableFixAssetMachineryCommandHandler : ICommandHandler<DisableFixAssetMachineryCommand, FixAssetMachinery>
{
    private readonly ILogger<DisableFixAssetMachineryCommand> _logger;
    private readonly IFixAssetMachineryRepository _repository;

    public DisableFixAssetMachineryCommandHandler(ILogger<DisableFixAssetMachineryCommand> logger, IFixAssetMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetMachinery?>> Handle(DisableFixAssetMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetFixAssetMachineryForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<FixAssetMachinery>(FixAssetMachineryErrors.FixAssetMachineryNotFoundWithId);
            if (entity.IsDeleted == true)
                return Result.Failure<FixAssetMachinery>(FixAssetMachineryErrors.IsDeleted);

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FixAssetMachinery>(SharedErrors.UnknownError);
        }
    }
}