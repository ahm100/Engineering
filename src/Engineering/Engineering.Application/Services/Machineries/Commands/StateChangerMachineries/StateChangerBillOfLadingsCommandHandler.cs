using Engineering.Application.Abstractions.Data.Machineries;

namespace Engineering.Application.Services.Machineries.Commands.StateChangerMachineries;

public class StateChangerMachineriesCommandHandler : ICommandHandler<StateChangerMachineriesCommand, bool?>
{
    private readonly ILogger<StateChangerMachineriesCommand> _logger;
    private readonly IMachineryRepository _repository;

    public StateChangerMachineriesCommandHandler(ILogger<StateChangerMachineriesCommand> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerMachineriesCommand request, CT ct)
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
                        item.SetDeactivate();
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