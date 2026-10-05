using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperationUnitOfMeasurement;

public class UpdateProjectOperationUnitOfMeasurementCommandHandler : ICommandHandler<UpdateProjectOperationUnitOfMeasurementCommand, List<ProjectOperation>>
{
    private readonly ILogger<UpdateProjectOperationUnitOfMeasurementCommand> _logger;
    private readonly IProjectOperationRepository _repository;
    private readonly IProjectOperationDependencyRepository _projectOperationDependencyRepository;

    public UpdateProjectOperationUnitOfMeasurementCommandHandler(ILogger<UpdateProjectOperationUnitOfMeasurementCommand> logger, IProjectOperationRepository repository,
        IProjectOperationDependencyRepository projectOperationDependencyRepository)
    {
        _logger = logger;
        _repository = repository;
        _projectOperationDependencyRepository = projectOperationDependencyRepository;
    }

    public async Task<Result<List<ProjectOperation>?>> Handle(UpdateProjectOperationUnitOfMeasurementCommand request, CT ct)
    {
        try
        {
            var entities = request.ProjectOperations;

            foreach (var item in entities)
            {
                item.SetUnitOfMeasurementId(request.UnitOfMeasurementId);
                await _repository.Update(item);
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectOperation>>(SharedErrors.UnknownError);
        }
    }
}