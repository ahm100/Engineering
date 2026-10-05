using Engineering.Application.Abstractions.Data.Projects;
using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Commands.UpdateProjectServiceStatus;

public class UpdateProjectServiceStatusCommandHandler : ICommandHandler<UpdateProjectServiceStatusCommand, ProjectService>
{
    private readonly ILogger<UpdateProjectServiceStatusCommand> _logger;
    private readonly IProjectServiceRepository _repository;

    public UpdateProjectServiceStatusCommandHandler(ILogger<UpdateProjectServiceStatusCommand> logger, IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectService?>> Handle(UpdateProjectServiceStatusCommand request, CT ct)
    {
        try
        {
            var entity = request.ProjectService;

            entity.SetStatus(request.Status);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ProjectService>(SharedErrors.UnknownError);
        }
    }
}