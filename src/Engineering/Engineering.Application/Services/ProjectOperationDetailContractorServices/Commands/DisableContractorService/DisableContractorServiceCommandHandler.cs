using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetailContractorService = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.DisableContractorService;

public class DisableContractorServiceCommandHandler : ICommandHandler<DisableContractorServiceCommand, ProjectOperationDetailContractorService>
{
    private readonly ILogger<DisableContractorServiceCommand> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public DisableContractorServiceCommandHandler(ILogger<DisableContractorServiceCommand> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailContractorService?>> Handle(DisableContractorServiceCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetailContractorService>(ContractorServiceErrors.ContractorServiceWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<ProjectOperationDetailContractorService>(ContractorServiceErrors.IsDeleted);
            if (entity.ContractorContractDetailServices.Any(x => !x.IsDeleted))
                return Result.Failure<ProjectOperationDetailContractorService>(ContractorServiceErrors.CanNottDeleteForContractorContractDetail);
            if (entity.DailyOperationServices.Any(x => !x.IsDeleted && x.DailyProjectOperation.ProjectOperationDetail.Id == request.projectOperationDetailId))
                return Result.Failure<ProjectOperationDetailContractorService>(ContractorServiceErrors.CanNottDeleteForDailyOperationServices);

            entity.SetIsDeleted();
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