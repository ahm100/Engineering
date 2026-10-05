using Engineering.Application.Abstractions.Data.Projects;
using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Commands.DisableProjectService;

public class DeleteProjectServiceCommandHandler : ICommandHandler<DeleteProjectServiceCommand, ProjectService>
{
    private readonly ILogger<DeleteProjectServiceCommand> _logger;
    private readonly IProjectServiceRepository _repository;

    public DeleteProjectServiceCommandHandler(ILogger<DeleteProjectServiceCommand> logger, IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectService?>> Handle(DeleteProjectServiceCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetProjectServiceById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectService>(ProjectServiceErrors.ProjectServiceWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<ProjectService>(ProjectServiceErrors.IsDeleted);

            foreach (var detail in entity.ProjectServiceDetails)
            {
                if (detail.ProjectOperationDetailContractorServices.Any())
                    return Result.Failure<ProjectService>(ProjectServiceErrors.HavePOD);
                if (detail.ProjectOperationDetailContractorServices.Any(x => x.ContractorContractDetailServices.Any()))
                    return Result.Failure<ProjectService>(ProjectServiceErrors.HaveContracts);
                if (detail.ProjectOperationDetailContractorServices.Any(x => x.DailyOperationServices.Any()))
                    return Result.Failure<ProjectService>(ProjectServiceErrors.HaveDaily);

                detail.SetIsDeleted();
            }

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectService>(SharedErrors.UnknownError);
        }
    }
}