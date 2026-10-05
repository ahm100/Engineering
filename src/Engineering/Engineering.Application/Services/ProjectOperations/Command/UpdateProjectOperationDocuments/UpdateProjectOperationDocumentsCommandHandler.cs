using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperationDocuments;

public class UpdateProjectOperationDocumentsCommandHandler : ICommandHandler<UpdateProjectOperationDocumentsCommand, ProjectOperation>
{
    private readonly ILogger<UpdateProjectOperationDocumentsCommand> _logger;
    private readonly IProjectOperationRepository _repository;
    private readonly IProjectOperationDependencyRepository _projectOperationDependencyRepository;

    public UpdateProjectOperationDocumentsCommandHandler(ILogger<UpdateProjectOperationDocumentsCommand> logger, IProjectOperationRepository repository,
        IProjectOperationDependencyRepository projectOperationDependencyRepository)
    {
        _logger = logger;
        _repository = repository;
        _projectOperationDependencyRepository = projectOperationDependencyRepository;
    }

    public async Task<Result<ProjectOperation?>> Handle(UpdateProjectOperationDocumentsCommand request, CT ct)
    {
        try
        {
            var entity = request.ProjectOperation;

            entity.AddDocuments(request.Urls);

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