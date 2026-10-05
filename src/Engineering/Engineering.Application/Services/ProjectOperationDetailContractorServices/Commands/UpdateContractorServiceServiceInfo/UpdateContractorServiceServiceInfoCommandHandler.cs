using Engineering.Application.Abstractions.Data.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.UpdateContractorServiceServiceInfo;

public class UpdateContractorServiceServiceInfoCommandHandler : ICommandHandler<UpdateContractorServiceServiceInfoCommand, bool>
{
    private readonly ILogger<UpdateContractorServiceServiceInfoCommand> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public UpdateContractorServiceServiceInfoCommandHandler(
        ILogger<UpdateContractorServiceServiceInfoCommand> logger,
        IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(UpdateContractorServiceServiceInfoCommand request, CT ct)
    {
        try
        {
            var contractorServices = request.ContractorServices;
            var operationInfoServices = request.OperationInfoServices;

            foreach (var entity in contractorServices)
            {
                var operationInfoService = operationInfoServices.FirstOrDefault(c => c.ServiceInfo.Id.Equals(entity.OperationInfoService.ServiceInfo.Id));
                if (operationInfoService is not null)
                {
                    entity.SetServiceInfo(operationInfoService);
                    await _repository.Update(entity);
                }
                else
                    return false;
            }

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}