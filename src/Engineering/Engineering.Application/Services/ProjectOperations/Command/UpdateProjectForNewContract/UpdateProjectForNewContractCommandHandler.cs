using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectForNewContract;

public class UpdateProjectForNewContractCommandHandler : ICommandHandler<UpdateProjectForNewContractCommand, ProjectOperation>
{
    private readonly ILogger<UpdateProjectForNewContractCommand> _logger;
    private readonly IProjectOperationRepository _repository;
    private readonly IProjectOperationDependencyRepository _projectOperationDependencyRepository;

    public UpdateProjectForNewContractCommandHandler(ILogger<UpdateProjectForNewContractCommand> logger, IProjectOperationRepository repository,
        IProjectOperationDependencyRepository projectOperationDependencyRepository)
    {
        _logger = logger;
        _repository = repository;
        _projectOperationDependencyRepository = projectOperationDependencyRepository;
    }

    public async Task<Result<ProjectOperation?>> Handle(UpdateProjectForNewContractCommand request, CT ct)
    {
        try
        {
            var entity = request.ProjectOperation;

            entity.SetProject(request.Project);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}
