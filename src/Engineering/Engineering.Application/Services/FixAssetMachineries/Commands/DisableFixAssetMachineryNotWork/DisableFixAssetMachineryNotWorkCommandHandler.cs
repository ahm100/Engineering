using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachineryNotWork = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryNotWork;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.DisableFixAssetMachineryNotWork;

public class DisableFixAssetMachineryNotWorkCommandHandler : ICommandHandler<DisableFixAssetMachineryNotWorkCommand, FixAssetMachineryNotWork>
{
    private readonly ILogger<DisableFixAssetMachineryNotWorkCommand> _logger;
    private readonly IFixAssetMachineryNotWorkRepository _repository;

    public DisableFixAssetMachineryNotWorkCommandHandler(ILogger<DisableFixAssetMachineryNotWorkCommand> logger, IFixAssetMachineryNotWorkRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetMachineryNotWork?>> Handle(DisableFixAssetMachineryNotWorkCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetFixAssetMachineryNotWorkForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<FixAssetMachineryNotWork>(FixAssetMachineryErrors.FixAssetMachineryNotWorkNotFoundWithId);
            if (entity.IsDeleted == true)
                return Result.Failure<FixAssetMachineryNotWork>(FixAssetMachineryErrors.NotWorkIsDeleted);

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FixAssetMachineryNotWork>(SharedErrors.UnknownError);
        }
    }
}