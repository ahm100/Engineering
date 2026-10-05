using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachineryRate = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryRate;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.DisableFixAssetMachineryRate;

public class DisableFixAssetMachineryRateCommandHandler : ICommandHandler<DisableFixAssetMachineryRateCommand, FixAssetMachineryRate>
{
    private readonly ILogger<DisableFixAssetMachineryRateCommand> _logger;
    private readonly IFixAssetMachineryRateRepository _repository;

    public DisableFixAssetMachineryRateCommandHandler(ILogger<DisableFixAssetMachineryRateCommand> logger, IFixAssetMachineryRateRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetMachineryRate?>> Handle(DisableFixAssetMachineryRateCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<FixAssetMachineryRate>(FixAssetMachineryErrors.RateNotfound);
            if (entity.IsDeleted == true)
                return Result.Failure<FixAssetMachineryRate>(FixAssetMachineryErrors.RateIsDeleted);

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FixAssetMachineryRate>(SharedErrors.UnknownError);
        }
    }
}