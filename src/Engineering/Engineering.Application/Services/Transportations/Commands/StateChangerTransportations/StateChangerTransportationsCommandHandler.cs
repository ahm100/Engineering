using Engineering.Application.Abstractions.Data.Transportations;

namespace Engineering.Application.Services.Transportations.Commands.StateChangerTransportations;

public class StateChangerTransportationsCommandHandler : ICommandHandler<StateChangerTransportationsCommand, bool?>
{
    private readonly ILogger<StateChangerTransportationsCommand> _logger;
    private readonly ITransportationRepository _repository;

    public StateChangerTransportationsCommandHandler(ILogger<StateChangerTransportationsCommand> logger, ITransportationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerTransportationsCommand request, CT ct)
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