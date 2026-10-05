using Engineering.Application.Abstractions.Data.ServiceInfos;

namespace Engineering.Application.Services.ServiceInfos.Commands.StateChangerServiceInfos;

public class StateChangerServiceInfosCommandHandler : ICommandHandler<StateChangerServiceInfosCommand, bool?>
{
    private readonly ILogger<StateChangerServiceInfosCommand> _logger;
    private readonly IServiceInfoRepository _repository;

    public StateChangerServiceInfosCommandHandler(ILogger<StateChangerServiceInfosCommand> logger, IServiceInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerServiceInfosCommand request, CT ct)
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