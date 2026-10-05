using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.DetailContractorServices.Commands.InactiveDetailContractorService;

public class InactiveDetailContractorServiceCommandHandler : ICommandHandler<InactiveDetailContractorServiceCommand, ProjectOperationDetailContractorService>
{
    private readonly ILogger<InactiveDetailContractorServiceCommand> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public InactiveDetailContractorServiceCommandHandler(ILogger<InactiveDetailContractorServiceCommand> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailContractorService?>> Handle(InactiveDetailContractorServiceCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetailContractorService>(ContractorServiceErrors.NotFound);
            if (entity.IsActive == false)
                return Result.Failure<ProjectOperationDetailContractorService>(ContractorServiceErrors.IsInactive);
            if (entity.IsDeleted == true)
                return Result.Failure<ProjectOperationDetailContractorService>(ContractorServiceErrors.IsDeleted);

            entity.SetInActive();
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