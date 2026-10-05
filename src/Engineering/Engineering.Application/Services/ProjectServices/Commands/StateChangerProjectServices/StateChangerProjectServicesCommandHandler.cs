using Engineering.Application.Abstractions.Data.Projects;

namespace Engineering.Application.Services.ProjectServices.Commands.StateChangerProjectServices;

public class StateChangerProjectServicesCommandHandler : ICommandHandler<StateChangerProjectServicesCommand, bool?>
{
    private readonly ILogger<StateChangerProjectServicesCommand> _logger;
    private readonly IProjectServiceRepository _repository;

    public StateChangerProjectServicesCommandHandler(ILogger<StateChangerProjectServicesCommand> logger, IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerProjectServicesCommand request, CT ct)
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