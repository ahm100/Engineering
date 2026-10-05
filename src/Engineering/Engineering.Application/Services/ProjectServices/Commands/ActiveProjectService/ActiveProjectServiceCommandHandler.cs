using Engineering.Application.Abstractions.Data.Projects;
using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Commands.ActiveProjectService;

public class ActiveProjectServiceCommandHandler : ICommandHandler<ActiveProjectServiceCommand, ProjectService>
{
    private readonly ILogger<ActiveProjectServiceCommand> _logger;
    private readonly IProjectServiceRepository _repository;

    public ActiveProjectServiceCommandHandler(ILogger<ActiveProjectServiceCommand> logger, IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectService?>> Handle(ActiveProjectServiceCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectService>(ProjectServiceErrors.ProjectServiceWithIdNotFound);
            if (entity.IsActive == true)
                return Result.Failure<ProjectService>(ProjectServiceErrors.IsActive);
            if (entity.IsDeleted == true)
                return Result.Failure<ProjectService>(ProjectServiceErrors.IsDeleted);

            entity.SetActive();

            await _repository.Update(entity);
            return entity;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectService>(SharedErrors.UnknownError);
        }
    }
}