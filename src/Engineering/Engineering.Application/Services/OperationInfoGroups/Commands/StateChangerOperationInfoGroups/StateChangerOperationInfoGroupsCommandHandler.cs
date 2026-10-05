using Engineering.Application.Abstractions.Data.OperationInfos;

namespace Engineering.Application.Services.OperationInfoGroups.Commands.StateChangerOperationInfoGroups;

public class StateChangerOperationInfoGroupsCommandHandler : ICommandHandler<StateChangerOperationInfoGroupsCommand, bool?>
{
    private readonly ILogger<StateChangerOperationInfoGroupsCommand> _logger;
    private readonly IOperationInfoGroupRepository _repository;

    public StateChangerOperationInfoGroupsCommandHandler(ILogger<StateChangerOperationInfoGroupsCommand> logger, IOperationInfoGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerOperationInfoGroupsCommand request, CT ct)
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