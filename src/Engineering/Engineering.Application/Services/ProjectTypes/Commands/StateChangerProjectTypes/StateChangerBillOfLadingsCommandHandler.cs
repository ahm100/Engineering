using Engineering.Application.Abstractions.Data.Projects;

namespace Engineering.Application.Services.ProjectTypes.Commands.StateChangerProjectTypes;

public class StateChangerProjectTypesCommandHandler : ICommandHandler<StateChangerProjectTypesCommand, bool?>
{
    private readonly ILogger<StateChangerProjectTypesCommand> _logger;
    private readonly IProjectTypeRepository _repository;

    public StateChangerProjectTypesCommandHandler(ILogger<StateChangerProjectTypesCommand> logger, IProjectTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerProjectTypesCommand request, CT ct)
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