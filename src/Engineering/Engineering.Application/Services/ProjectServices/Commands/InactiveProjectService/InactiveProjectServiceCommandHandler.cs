using Engineering.Application.Abstractions.Data.Projects;
using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Commands.InactiveProjectService;

public class InactiveProjectServiceCommandHandler : ICommandHandler<InactiveProjectServiceCommand, ProjectService>
{
    private readonly ILogger<InactiveProjectServiceCommand> _logger;
    private readonly IProjectServiceRepository _repository;

    public InactiveProjectServiceCommandHandler(ILogger<InactiveProjectServiceCommand> logger, IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectService?>> Handle(InactiveProjectServiceCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectService>(ProjectServiceErrors.ProjectServiceWithIdNotFound);
            if (entity.IsActive == false)
                return Result.Failure<ProjectService>(ProjectServiceErrors.IsInactive);
            if (entity.IsDeleted == true)
                return Result.Failure<ProjectService>(ProjectServiceErrors.IsDeleted);

            entity.SetInActive();

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