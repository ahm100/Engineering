using Engineering.Application.Abstractions.Data.Projects;
using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Commands.UpdateProjectType;

public class UpdateProjectTypeCommandHandler : ICommandHandler<UpdateProjectTypeCommand, ProjectType>
{
    private readonly ILogger<UpdateProjectTypeCommand> _logger;
    private readonly IProjectTypeRepository _repository;

    public UpdateProjectTypeCommandHandler(ILogger<UpdateProjectTypeCommand> logger, IProjectTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectType?>> Handle(UpdateProjectTypeCommand request, CT ct)
    {
        try
        {
            var ProjectTypeEntity = await _repository.FindById(request.Id, ct);
            if (ProjectTypeEntity is null)
                return Result.Failure<ProjectType>(ProjectErrors.ProjectWithIdNotFound);

            ProjectTypeEntity.SetName(request.ProjectTypeName);
            ProjectTypeEntity.SetCode(request.ProjectTypeCode);
            ProjectTypeEntity.SetCompanyId(request.CompanyId);

            if (request.IsActive == false)
                ProjectTypeEntity.SetDeactivate();
            else
                ProjectTypeEntity.SetActive();

            await _repository.Update(ProjectTypeEntity);

            return ProjectTypeEntity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ProjectType>(SharedErrors.UnknownError);
        }
    }
}