using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.InactiveFixAssetMachinery;

public class InactiveFixAssetMachineryCommandHandler : ICommandHandler<InactiveFixAssetMachineryCommand, FixAssetMachinery>
{
    private readonly ILogger<InactiveFixAssetMachineryCommand> _logger;
    private readonly IFixAssetMachineryRepository _repository;

    public InactiveFixAssetMachineryCommandHandler(ILogger<InactiveFixAssetMachineryCommand> logger, IFixAssetMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetMachinery?>> Handle(InactiveFixAssetMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
            {
                return Result.Failure<FixAssetMachinery>(FixAssetMachineryErrors.FixAssetMachineryNotFoundWithId);
            }
            if (entity.IsActive == false)
            {
                return Result.Failure<FixAssetMachinery>(FixAssetMachineryErrors.InValidInActivate);
            }
            if (entity.IsDeleted == true)
            {
                return Result.Failure<FixAssetMachinery>(FixAssetMachineryErrors.IsDeleted);
            }

            entity.SetInActive();

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