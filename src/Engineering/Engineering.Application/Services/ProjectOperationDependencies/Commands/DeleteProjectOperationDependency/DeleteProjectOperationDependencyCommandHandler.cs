using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperationDependency = Engineering.Domain.Entities.ProjectOperations.ProjectOperationDependency;

namespace Engineering.Application.Services.ProjectOperationDependencies.Commands.DeleteProjectOperationDependency;

public class DeleteProjectOperationDependencyCommandHandler : ICommandHandler<DeleteProjectOperationDependencyCommand, ProjectOperationDependency>
{
    private readonly ILogger<DeleteProjectOperationDependencyCommand> _logger;
    private readonly IProjectOperationDependencyRepository _repository;

    public DeleteProjectOperationDependencyCommandHandler(ILogger<DeleteProjectOperationDependencyCommand> logger, IProjectOperationDependencyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDependency?>> Handle(DeleteProjectOperationDependencyCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDependency>(ProjectOperationDependencyErrors.FilteredDependencyNotFound);
            if (entity.IsDeleted)
                return Result.Failure<ProjectOperationDependency>(ProjectOperationDependencyErrors.IsDeleted);

            entity.SoftDelete();
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ProjectOperationDependency>(SharedErrors.UnknownError);
        }
    }
}