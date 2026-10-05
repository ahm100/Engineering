using Engineering.Application.Abstractions.Data.Projects;
using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Commands.ActiveProjectType;

public class ActiveProjectTypeCommandHandler : ICommandHandler<ActiveProjectTypeCommand, ProjectType>
{
    private readonly ILogger<ActiveProjectTypeCommand> _logger;
    private readonly IProjectTypeRepository _repository;

    public ActiveProjectTypeCommandHandler(ILogger<ActiveProjectTypeCommand> logger, IProjectTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectType?>> Handle(ActiveProjectTypeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectType>(ProjectTypeErrors.ProjectTypeWithIdNotFound);
            if (entity.IsActive == true)
                return Result.Failure<ProjectType>(ProjectTypeErrors.IsActive);
            if (entity.IsDeleted == true)
                return Result.Failure<ProjectType>(ProjectTypeErrors.IsDeleted);

            entity.SetActive();

            await _repository.Update(entity);
            return entity;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectType>(SharedErrors.UnknownError);
        }
    }
}