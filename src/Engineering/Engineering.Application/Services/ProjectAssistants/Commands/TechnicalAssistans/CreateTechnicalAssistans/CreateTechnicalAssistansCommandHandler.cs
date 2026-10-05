using Engineering.Application.Abstractions.Data.Projects;
using ProjectTechnicalAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectTechnicalAssistant;

namespace Engineering.Application.Services.ProjectAssistants.Commands.TechnicalAssistans.CreateTechnicalAssistans;

public class CreateTechnicalAssistansCommandHandler : ICommandHandler<CreateTechnicalAssistansCommand, ProjectTechnicalAssistant>
{
    private readonly ILogger<CreateTechnicalAssistansCommand> _logger;
    private readonly IProjectTechnicalAssistantRepository _repository;

    public CreateTechnicalAssistansCommandHandler(ILogger<CreateTechnicalAssistansCommand> logger, IProjectTechnicalAssistantRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectTechnicalAssistant?>> Handle(CreateTechnicalAssistansCommand request, CT ct)
    {
        try
        {
            var newExpert = new ProjectTechnicalAssistant(request.Project, request.TechnicalAssistantUserId);
            var result = await _repository.Create(newExpert, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectTechnicalAssistant>(SharedErrors.UnknownError);
        }
    }
}