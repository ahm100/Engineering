using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.DetailContractorServices.Commands.ActiveDetailContractorService;

public class ActiveDetailContractorServiceCommandHandler : ICommandHandler<ActiveDetailContractorServiceCommand, ProjectOperationDetailContractorService>
{
    private readonly ILogger<ActiveDetailContractorServiceCommand> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public ActiveDetailContractorServiceCommandHandler(
        ILogger<ActiveDetailContractorServiceCommand> logger,
        IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailContractorService?>> Handle(ActiveDetailContractorServiceCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetailContractorService>(ContractorServiceErrors.ContractorServiceWithIdNotFound);
            if (entity.IsActive == true)
                return Result.Failure<ProjectOperationDetailContractorService>(ContractorServiceErrors.IsActive);
            if (entity.IsDeleted == true)
                return Result.Failure<ProjectOperationDetailContractorService>(ContractorServiceErrors.IsDeleted);

            entity.SetActive();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetailContractorService>(SharedErrors.UnknownError);
        }
    }
}