using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Commands.ActiveProject;

public class ActiveProjectCommandHandler : ICommandHandler<ActiveProjectCommand, Project>
{
    private readonly ILogger<ActiveProjectCommand> _logger;
    private readonly IProjectRepository _repository;

    public ActiveProjectCommandHandler(ILogger<ActiveProjectCommand> logger, IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Project?>> Handle(ActiveProjectCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
            {
                return Result.Failure<Project>(ProjectErrors.ProjectWithIdNotFound);
            }
            if (entity.IsActive == true)
            {
                return Result.Failure<Project>(ProjectErrors.IsActive);
            }
            if (entity.IsDeleted == true)
            {
                return Result.Failure<Project>(ProjectErrors.IsDeleted);
            }

            entity.SetActive();
            entity.AddHistory();
            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Project>(SharedErrors.UnknownError);
        }
    }
}