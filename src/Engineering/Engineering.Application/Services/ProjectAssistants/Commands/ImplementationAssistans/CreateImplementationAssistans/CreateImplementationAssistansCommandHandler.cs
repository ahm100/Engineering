using Engineering.Application.Abstractions.Data.Projects;
using ProjectImplementationAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectImplementationAssistant;

namespace Engineering.Application.Services.ProjectAssistants.Commands.ImplementationAssistans.CreateImplementationAssistans;

public class CreateImplementationAssistansCommandHandler : ICommandHandler<CreateImplementationAssistansCommand, ProjectImplementationAssistant>
{
    private readonly ILogger<CreateImplementationAssistansCommand> _logger;
    private readonly IProjectImplementationAssistantRepository _repository;

    public CreateImplementationAssistansCommandHandler(ILogger<CreateImplementationAssistansCommand> logger, IProjectImplementationAssistantRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectImplementationAssistant?>> Handle(CreateImplementationAssistansCommand request, CT ct)
    {
        try
        {
            var newExpert = new ProjectImplementationAssistant(request.Project, request.ImplementationAssistantUserId);
            var result = await _repository.Create(newExpert, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectImplementationAssistant>(SharedErrors.UnknownError);
        }
    }
}