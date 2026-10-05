using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.ActiveFixAssetMachinery;

public class ActiveFixAssetMachineryCommandHandler : ICommandHandler<ActiveFixAssetMachineryCommand, FixAssetMachinery>
{
    private readonly ILogger<ActiveFixAssetMachineryCommand> _logger;
    private readonly IFixAssetMachineryRepository _repository;

    public ActiveFixAssetMachineryCommandHandler(ILogger<ActiveFixAssetMachineryCommand> logger, IFixAssetMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetMachinery?>> Handle(ActiveFixAssetMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
            {
                return Result.Failure<FixAssetMachinery>(FixAssetMachineryErrors.FixAssetMachineryNotFoundWithId);
            }
            if (entity.IsActive == true)
            {
                return Result.Failure<FixAssetMachinery>(FixAssetMachineryErrors.InValidActivate);
            }
            if (entity.IsDeleted == true)
            {
                return Result.Failure<FixAssetMachinery>(FixAssetMachineryErrors.IsDeleted);
            }

            entity.SetActive();

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