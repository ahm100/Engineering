using Engineering.Application.Abstractions.Data.FixAssetMachineries;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.StateChangerFixAssetMachineries;

public class StateChangerFixAssetMachineriesCommandHandler : ICommandHandler<StateChangerFixAssetMachineriesCommand, bool?>
{
    private readonly ILogger<StateChangerFixAssetMachineriesCommand> _logger;
    private readonly IFixAssetMachineryRepository _repository;

    public StateChangerFixAssetMachineriesCommandHandler(ILogger<StateChangerFixAssetMachineriesCommand> logger, IFixAssetMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerFixAssetMachineriesCommand request, CT ct)
    {
        try
        {
            if (request.State)
                foreach (var item in request.Items)
                {
                    if (item.IsActive != request.State)
                    {
                        item.SetActive();
                        await _repository.Update(item);
                    }
                }
            else
                foreach (var item in request.Items)
                {
                    if (item.IsActive != request.State)
                    {
                        item.SetInActive();
                        await _repository.Update(item);
                    }
                }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}