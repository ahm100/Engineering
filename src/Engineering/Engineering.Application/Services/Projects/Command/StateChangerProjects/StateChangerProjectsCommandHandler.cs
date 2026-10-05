using Engineering.Application.Abstractions.Data.Projects;

namespace Engineering.Application.Services.Projects.Commands.StateChangerProjects;

public class StateChangerProjectsCommandHandler : ICommandHandler<StateChangerProjectsCommand, bool?>
{
    private readonly ILogger<StateChangerProjectsCommand> _logger;
    private readonly IProjectRepository _repository;

    public StateChangerProjectsCommandHandler(ILogger<StateChangerProjectsCommand> logger, IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerProjectsCommand request, CT ct)
    {
        try
        {
            if (request.State)
                foreach (var item in request.Items)
                {
                    if (item.IsActive != request.State)
                    {
                        item.SetActive();
                        item.AddHistory();
                        await _repository.Update(item);
                    }
                }
            else
                foreach (var item in request.Items)
                {
                    if (item.IsActive != request.State)
                    {
                        item.SetInActive();
                        item.AddHistory();
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