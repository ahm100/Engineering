using Engineering.Application.Abstractions.Data.Projects;
using ProjectTechnicalAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectTechnicalAssistant;

namespace Engineering.Application.Services.ProjectAssistants.Commands.TechnicalAssistans.DeleteTechnicalAssistans;

public class DeleteTechnicalAssistansCommandHandler : ICommandHandler<DeleteTechnicalAssistansCommand, ProjectTechnicalAssistant>
{
    private readonly ILogger<DeleteTechnicalAssistansCommand> _logger;
    private readonly IProjectTechnicalAssistantRepository _repository;

    public DeleteTechnicalAssistansCommandHandler(ILogger<DeleteTechnicalAssistansCommand> logger, IProjectTechnicalAssistantRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectTechnicalAssistant?>> Handle(DeleteTechnicalAssistansCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindUser(request.TechnicalAssistantId, request.ProjectId, ct);
            if (entity is null)
            {
                return Result.Failure<ProjectTechnicalAssistant>(ProjectAssistantErrors.ProjectAssistanWithIdNotFound);
            }
            if (entity.IsDeleted)
            {
                return Result.Failure<ProjectTechnicalAssistant>(ProjectAssistantErrors.IsDeletedTecnical);
            }

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectTechnicalAssistant>(SharedErrors.UnknownError);
        }
    }
}