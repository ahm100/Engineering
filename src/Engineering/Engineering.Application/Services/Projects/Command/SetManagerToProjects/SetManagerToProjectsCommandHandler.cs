using Engineering.Application.Abstractions.Data.Projects;

namespace Engineering.Application.Services.Projects.Commands.SetManagerToProjects;

public class SetManagerToProjectsCommandHandler : ICommandHandler<SetManagerToProjectsCommand, bool>
{
    private readonly ILogger<SetManagerToProjectsCommand> _logger;
    private readonly IProjectRepository _repository;

    public SetManagerToProjectsCommandHandler(ILogger<SetManagerToProjectsCommand> logger, IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(SetManagerToProjectsCommand request, CT ct)
    {
        try
        {
            if (request.Projects is not null && request.Projects.Count > 0)
                foreach (var item in request.Projects)
                {
                    item.SetProjectManager(request.ProjectManager);
                    item.AddHistory();
                    await _repository.Update(item);
                }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}