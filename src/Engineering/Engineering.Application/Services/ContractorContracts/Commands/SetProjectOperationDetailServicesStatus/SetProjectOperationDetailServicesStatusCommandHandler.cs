using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ContractorContracts.Commands.SetProjectOperationDetailServicesStatus;

public class SetProjectOperationDetailServicesStatusCommandHandler : ICommandHandler<SetProjectOperationDetailServicesStatusCommand, ProjectOperationDetailContractorService>
{
    private ILogger<SetProjectOperationDetailServicesStatusCommandHandler> _logger;
    private IProjectOperationDetailContractorServiceRepository _repository;

    public SetProjectOperationDetailServicesStatusCommandHandler(
        ILogger<SetProjectOperationDetailServicesStatusCommandHandler> logger,
        IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailContractorService?>> Handle(SetProjectOperationDetailServicesStatusCommand request, CT ct)
    {
        try
        {
            var result = await _repository.FindById(request.Id, ct);
            if (result is null)
                return Result.Failure<ProjectOperationDetailContractorService>(ContractorContractErrors.InValidProjectOperationServiceId);

            result.SetStatus(request.Status);
            await _repository.Update(result);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetailContractorService>(SharedErrors.UnknownError);
        }
    }
}
