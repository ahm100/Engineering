using Engineering.Application.Abstractions.Data.Machineries;

namespace Engineering.Application.Services.MachineriesGroups.Commands.StateChangerMachineriesGroups;

public class StateChangerMachineriesGroupsCommandHandler : ICommandHandler<StateChangerMachineriesGroupsCommand, bool?>
{
    private readonly ILogger<StateChangerMachineriesGroupsCommand> _logger;
    private readonly IMachineriesGroupRepository _repository;

    public StateChangerMachineriesGroupsCommandHandler(ILogger<StateChangerMachineriesGroupsCommand> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerMachineriesGroupsCommand request, CT ct)
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