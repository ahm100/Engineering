using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectServices.Commands.DeleteProjectServiceDetail;

public class DeleteProjectServiceDetailCommandHandler : ICommandHandler<DeleteProjectServiceDetailCommand, ProjectServiceDetail>
{
    private readonly ILogger<DeleteProjectServiceDetailCommand> _logger;
    private readonly IProjectServiceDetailRepository _repository;

    public DeleteProjectServiceDetailCommandHandler(ILogger<DeleteProjectServiceDetailCommand> logger, IProjectServiceDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectServiceDetail?>> Handle(DeleteProjectServiceDetailCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetProjectServiceDetailById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectServiceDetail>(ProjectServiceErrors.ProjectServiceWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<ProjectServiceDetail>(ProjectServiceErrors.IsDeleted);
            if (entity.ProjectOperationDetailContractorServices.Any())
                return Result.Failure<ProjectServiceDetail>(ProjectServiceErrors.HavePOD);
            if (entity.ProjectOperationDetailContractorServices.Any(x => x.ContractorContractDetailServices.Any()))
                return Result.Failure<ProjectServiceDetail>(ProjectServiceErrors.HaveContracts);
            if (entity.ProjectOperationDetailContractorServices.Any(x => x.DailyOperationServices.Any()))
                return Result.Failure<ProjectServiceDetail>(ProjectServiceErrors.HaveDaily);

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectServiceDetail>(SharedErrors.UnknownError);
        }
    }
}