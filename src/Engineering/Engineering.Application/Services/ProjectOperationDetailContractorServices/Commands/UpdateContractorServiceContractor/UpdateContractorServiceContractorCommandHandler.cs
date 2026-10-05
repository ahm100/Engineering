using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetailContractorService = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.UpdateContractorServiceContractor;

public class UpdateContractorServiceContractorCommandHandler : ICommandHandler<UpdateContractorServiceContractorCommand, ProjectOperationDetailContractorService>
{
    private readonly ILogger<UpdateContractorServiceContractorCommand> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public UpdateContractorServiceContractorCommandHandler(ILogger<UpdateContractorServiceContractorCommand> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailContractorService?>> Handle(UpdateContractorServiceContractorCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetailContractorService>(ContractorServiceErrors.ContractorServiceWithIdNotFound);

            entity.SetContractorId(request.ContractorId);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ProjectOperationDetailContractorService>(SharedErrors.UnknownError);
        }
    }
}