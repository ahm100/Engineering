using Engineering.Application.Abstractions.Data.Branchs;

namespace Engineering.Application.Services.Branchs.Commands.StateChangerBranchs;

public class StateChangerBranchsCommandHandler : ICommandHandler<StateChangerBranchsCommand, bool?>
{
    private readonly ILogger<StateChangerBranchsCommand> _logger;
    private readonly IBranchRepository _repository;

    public StateChangerBranchsCommandHandler(
        ILogger<StateChangerBranchsCommand> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerBranchsCommand request, CT ct)
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