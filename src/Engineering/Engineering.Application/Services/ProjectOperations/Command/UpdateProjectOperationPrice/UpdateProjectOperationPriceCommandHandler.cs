using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperationPrice;

public class UpdateProjectOperationPriceCommandHandler : ICommandHandler<UpdateProjectOperationPriceCommand, ProjectOperation>
{
    private readonly ILogger<UpdateProjectOperationPriceCommand> _logger;
    private readonly IProjectOperationRepository _repository;
    private readonly IProjectOperationDependencyRepository _projectOperationDependencyRepository;

    public UpdateProjectOperationPriceCommandHandler(ILogger<UpdateProjectOperationPriceCommand> logger, IProjectOperationRepository repository,
        IProjectOperationDependencyRepository projectOperationDependencyRepository)
    {
        _logger = logger;
        _repository = repository;
        _projectOperationDependencyRepository = projectOperationDependencyRepository;
    }

    public async Task<Result<ProjectOperation?>> Handle(UpdateProjectOperationPriceCommand request, CT ct)
    {
        try
        {
            var entity = request.ProjectOperation;

            entity.SetPrice(request.Price);

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