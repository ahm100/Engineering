using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachineryNotWork = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryNotWork;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.UpdateFixAssetMachineryNotWork;

public class UpdateFixAssetMachineryNotWorkCommandHandler : ICommandHandler<UpdateFixAssetMachineryNotWorkCommand, FixAssetMachineryNotWork>
{
    private readonly ILogger<UpdateFixAssetMachineryNotWorkCommand> _logger;
    private readonly IFixAssetMachineryNotWorkRepository _repository;

    public UpdateFixAssetMachineryNotWorkCommandHandler(ILogger<UpdateFixAssetMachineryNotWorkCommand> logger, IFixAssetMachineryNotWorkRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetMachineryNotWork?>> Handle(UpdateFixAssetMachineryNotWorkCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<FixAssetMachineryNotWork>(FixAssetMachineryErrors.FixAssetMachineryNotWorkNotFoundWithId);

            entity.SetDescription(request.Description);
            entity.SetStartDate(request.StartDate);
            entity.SetEndDate(request.EndDate);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<FixAssetMachineryNotWork>(SharedErrors.UnknownError);
        }
    }
}