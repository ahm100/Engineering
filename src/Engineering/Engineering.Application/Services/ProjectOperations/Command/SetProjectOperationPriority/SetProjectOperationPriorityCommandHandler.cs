using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.SetProjectOperationPriority;

public class SetProjectOperationPriorityCommandHandler : ICommandHandler<SetProjectOperationPriorityCommand, ProjectOperation>
{
    private readonly ILogger<SetProjectOperationPriorityCommand> _logger;
    private readonly IProjectOperationRepository _repository;
    private readonly IProjectOperationDependencyRepository _projectOperationDependencyRepository;

    public SetProjectOperationPriorityCommandHandler(
        ILogger<SetProjectOperationPriorityCommand> logger,
        IProjectOperationRepository repository,
        IProjectOperationDependencyRepository projectOperationDependencyRepository)
    {
        _logger = logger;
        _repository = repository;
        _projectOperationDependencyRepository = projectOperationDependencyRepository;
    }

    public async Task<Result<ProjectOperation?>> Handle(SetProjectOperationPriorityCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);

            if (request.Priority == 1 && entity.SuccessorProjectOperationDependencies is not null)
                foreach (var dependency in entity.SuccessorProjectOperationDependencies)
                    await _projectOperationDependencyRepository.Remove(dependency);

            entity.SetPriority(request.Priority);
            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}