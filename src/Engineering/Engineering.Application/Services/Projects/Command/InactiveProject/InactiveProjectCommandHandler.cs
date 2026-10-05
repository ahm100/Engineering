using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Commands.InactiveProject;

public class InactiveProjectCommandHandler : ICommandHandler<InactiveProjectCommand, Project>
{
    private readonly ILogger<InactiveProjectCommand> _logger;
    private readonly IProjectRepository _repository;

    public InactiveProjectCommandHandler(ILogger<InactiveProjectCommand> logger, IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Project?>> Handle(InactiveProjectCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
            {
                return Result.Failure<Project>(ProjectErrors.ProjectWithIdNotFound);
            }
            if (entity.IsActive == false)
            {
                return Result.Failure<Project>(ProjectErrors.IsInactive);
            }
            if (entity.IsDeleted == true)
            {
                return Result.Failure<Project>(ProjectErrors.IsDeleted);
            }

            entity.SetInActive();
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