using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperation;

public class UpdateProjectOperationCommandHandler : ICommandHandler<UpdateProjectOperationCommand, ProjectOperation>
{
    private readonly ILogger<UpdateProjectOperationCommand> _logger;
    private readonly IProjectOperationRepository _repository;
    private readonly IProjectOperationDependencyRepository _projectOperationDependencyRepository;

    public UpdateProjectOperationCommandHandler(ILogger<UpdateProjectOperationCommand> logger, IProjectOperationRepository repository,
        IProjectOperationDependencyRepository projectOperationDependencyRepository)
    {
        _logger = logger;
        _repository = repository;
        _projectOperationDependencyRepository = projectOperationDependencyRepository;
    }

    public async Task<Result<ProjectOperation?>> Handle(UpdateProjectOperationCommand request, CT ct)
    {
        try
        {
            var entity = request.ProjectOperation;

            entity.SetWorkload(request.Workload);
            entity.SetProject(request.Project);
            entity.SetTolerancePercentage(request.TolerancePercentage);
            entity.SetPrice(request.Price);
            entity.SetPriority(request.Priority);
            entity.SetDescription(request.Description);
            entity.SetChangedPrice(request.ChangedPrice);
            ///TODO Employers
            //entity.SetEmployerContract(request.EmployerContract);
            entity.SetUnitOfMeasurementId(request.UnitOfMeasurementId);
            entity.SetCompanyId(request.CompanyId);
            entity.AddDocuments(request.Urls);

            if (request.OperationInfo is not null)
                entity.SetOperationInfo(request.OperationInfo);

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