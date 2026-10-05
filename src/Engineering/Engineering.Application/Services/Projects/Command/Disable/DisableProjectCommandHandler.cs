using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Commands.Disable;

public class DisableProjectCommandHandler : ICommandHandler<DisableProjectCommand, Project>
{
    private readonly ILogger<DisableProjectCommand> _logger;
    private readonly IProjectRepository _repository;
    private readonly ISubProjectRepository _subProjectRepository;

    public DisableProjectCommandHandler(ILogger<DisableProjectCommand> logger, IProjectRepository repository,
        ISubProjectRepository subProjectRepository)
    {
        _logger = logger;
        _repository = repository;
        _subProjectRepository = subProjectRepository;
    }

    public async Task<Result<Project?>> Handle(DisableProjectCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<Project>(ProjectErrors.ProjectWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<Project>(ProjectErrors.IsDeleted);
            if (entity.EmployerContracts.Count > 0)
                return Result.Failure<Project>(ProjectErrors.CanNotDeleteForEmContracts);
            if (entity.ProjectOperations.Count > 0)
                return Result.Failure<Project>(ProjectErrors.CanNotDeleteForProjectOpration);
            if (entity.FiduciaryProducts.Count > 0)
                return Result.Failure<Project>(ProjectErrors.CanNotDeleteForFidProducts);
            if (entity.ProjectOperations.Any(x => x.RequestGoodsSupplies.Count > 0))
                return Result.Failure<Project>(ProjectErrors.CanNotDeleteForReqGoodsSupplies);
            if (await _subProjectRepository.ProjectHasSubProjects(entity.Id, ct))
                return Result.Failure<Project>(ProjectErrors.CanNotDelete);

            entity.SetIsDeleted();
            entity.AddHistory();
            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Project>(SharedErrors.UnknownError);
        }
    }
}
