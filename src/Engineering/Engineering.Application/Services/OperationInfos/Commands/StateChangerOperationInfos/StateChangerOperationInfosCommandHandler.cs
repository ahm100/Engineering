using Engineering.Application.Abstractions.Data.OperationInfos;

namespace Engineering.Application.Services.OperationInfos.Commands.StateChangerOperationInfos;

public class StateChangerOperationInfosCommandHandler : ICommandHandler<StateChangerOperationInfosCommand, bool?>
{
    private readonly ILogger<StateChangerOperationInfosCommand> _logger;
    private readonly IOperationInfoRepository _repository;

    public StateChangerOperationInfosCommandHandler(ILogger<StateChangerOperationInfosCommand> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerOperationInfosCommand request, CT ct)
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