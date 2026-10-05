using Engineering.Application.Abstractions.Data.Projects;
using ProjectImplementationAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectImplementationAssistant;

namespace Engineering.Application.Services.ProjectAssistants.Commands.ImplementationAssistans.DeleteImplementationAssistans;

public class DeleteImplementationAssistansCommandHandler : ICommandHandler<DeleteImplementationAssistansCommand, ProjectImplementationAssistant>
{
    private readonly ILogger<DeleteImplementationAssistansCommand> _logger;
    private readonly IProjectImplementationAssistantRepository _repository;

    public DeleteImplementationAssistansCommandHandler(ILogger<DeleteImplementationAssistansCommand> logger, IProjectImplementationAssistantRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectImplementationAssistant?>> Handle(DeleteImplementationAssistansCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindUser(request.ImplementationAssistansId, request.ProjectId, ct);
            if (entity is null)
            {
                return Result.Failure<ProjectImplementationAssistant>(ProjectAssistantErrors.ProjectAssistanWithIdNotFound);
            }
            if (entity.IsDeleted)
            {
                return Result.Failure<ProjectImplementationAssistant>(ProjectAssistantErrors.IsDeleted);
            }

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectImplementationAssistant>(SharedErrors.UnknownError);
        }
    }
}