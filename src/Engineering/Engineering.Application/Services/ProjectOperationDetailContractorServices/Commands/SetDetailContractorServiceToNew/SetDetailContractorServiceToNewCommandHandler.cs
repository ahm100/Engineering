using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.SetDetailContractorServiceToNew;

public class SetDetailContractorServiceToNewCommandHandler : ICommandHandler<SetDetailContractorServiceToNewCommand, ProjectOperationDetailContractorService>
{
    private readonly ILogger<SetDetailContractorServiceToNewCommand> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public SetDetailContractorServiceToNewCommandHandler(ILogger<SetDetailContractorServiceToNewCommand> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailContractorService?>> Handle(SetDetailContractorServiceToNewCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetailContractorService>(ContractorServiceErrors.NotFound);

            entity.SetStatus(ContractorServiceStatus.New);
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
