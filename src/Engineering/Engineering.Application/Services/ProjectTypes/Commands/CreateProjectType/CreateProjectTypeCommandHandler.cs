using Engineering.Application.Abstractions.Data.Projects;
using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Commands.CreateProjectType;

public class CreateProjectTypeCommandHandler : ICommandHandler<CreateProjectTypeCommand, ProjectType>
{
    private readonly ILogger<CreateProjectTypeCommand> _logger;
    private readonly IProjectTypeRepository _repository;

    public CreateProjectTypeCommandHandler(ILogger<CreateProjectTypeCommand> logger, IProjectTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectType?>> Handle(CreateProjectTypeCommand request, CT ct)
    {
        try
        {
            var newProjectType = new ProjectType(request.ProjectTypeName,
                request.ProjectTypeCode,
                request.IsActive,
                request.CompanyId);
            var result = await _repository.Create(newProjectType, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectType>(SharedErrors.UnknownError);
        }
    }
}