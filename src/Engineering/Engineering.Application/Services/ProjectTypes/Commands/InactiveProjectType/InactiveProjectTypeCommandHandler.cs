using Engineering.Application.Abstractions.Data.Projects;
using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Commands.InactiveProjectType;

public class InactiveProjectTypeCommandHandler : ICommandHandler<InactiveProjectTypeCommand, ProjectType>
{
    private readonly ILogger<InactiveProjectTypeCommand> _logger;
    private readonly IProjectTypeRepository _repository;

    public InactiveProjectTypeCommandHandler(ILogger<InactiveProjectTypeCommand> logger, IProjectTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectType?>> Handle(InactiveProjectTypeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectType>(ProjectTypeErrors.ProjectTypeWithIdNotFound);
            if (entity.IsActive == false)
                return Result.Failure<ProjectType>(ProjectTypeErrors.IsInactive);
            if (entity.IsDeleted == true)
                return Result.Failure<ProjectType>(ProjectTypeErrors.IsDeleted);

            entity.SetDeactivate();

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