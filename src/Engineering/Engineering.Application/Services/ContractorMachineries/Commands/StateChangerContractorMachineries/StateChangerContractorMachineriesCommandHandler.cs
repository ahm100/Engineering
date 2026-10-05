using Engineering.Application.Abstractions.Data.ContractorMachineries;

namespace Engineering.Application.Services.ContractorMachineries.Commands.StateChangerContractorMachineries;

public class StateChangerContractorMachineriesCommandHandler : ICommandHandler<StateChangerContractorMachineriesCommand, bool?>
{
    private readonly ILogger<StateChangerContractorMachineriesCommand> _logger;
    private readonly IContractorMachineryRepository _repository;

    public StateChangerContractorMachineriesCommandHandler(ILogger<StateChangerContractorMachineriesCommand> logger, IContractorMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerContractorMachineriesCommand request, CT ct)
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