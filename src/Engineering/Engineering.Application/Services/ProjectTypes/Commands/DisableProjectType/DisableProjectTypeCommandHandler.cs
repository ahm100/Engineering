using Engineering.Application.Abstractions.Data.Projects;
using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Commands.DisableProjectType;

public class DisableProjectTypeCommandHandler : ICommandHandler<DisableProjectTypeCommand, ProjectType>
{
    private readonly ILogger<DisableProjectTypeCommand> _logger;
    private readonly IProjectTypeRepository _repository;

    public DisableProjectTypeCommandHandler(ILogger<DisableProjectTypeCommand> logger, IProjectTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectType?>> Handle(DisableProjectTypeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectType>(ProjectErrors.ProjectWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<ProjectType>(ProjectErrors.IsDeletedForType);
            if (entity.Projects.Count > 0)
                return Result.Failure<ProjectType>(ProjectErrors.CanNotDeleteForProject);

            entity.SoftDelete();

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