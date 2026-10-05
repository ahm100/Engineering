using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.CreateContractorService;

public class CreateContractorServiceCommandHandler : ICommandHandler<CreateContractorServiceCommand, ProjectOperationDetailContractorService>
{
    private readonly ILogger<CreateContractorServiceCommandHandler> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public CreateContractorServiceCommandHandler(
        ILogger<CreateContractorServiceCommandHandler> logger,
        IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }


    public async Task<Result<ProjectOperationDetailContractorService?>> Handle(
    CreateContractorServiceCommand request,
    CT ct)
    {
        try
        {
            var contractorServices =
                request.ProjectOperationDetail
                    .ProjectOperationDetailContractorServices;

            // --------------------------------------------------
            // preventing (ServiceBased + OperationBased)
            // --------------------------------------------------
            if (request.Type == PODContractorServiceType.ServiceBased &&
                contractorServices.Any(x =>
                    !x.IsDeleted &&
                    x.Type == PODContractorServiceType.OperationBased))
            {
                return Result.Failure<ProjectOperationDetailContractorService>(
                    ContractorServiceErrors.MixedContractorServiceTypeNotAllowed);
            }

            if (request.Type == PODContractorServiceType.OperationBased &&
                contractorServices.Any(x =>
                    !x.IsDeleted &&
                    x.Type == PODContractorServiceType.ServiceBased))
            {
                return Result.Failure<ProjectOperationDetailContractorService>(
                    ContractorServiceErrors.MixedContractorServiceTypeNotAllowed);
            }

            if (request.Type == PODContractorServiceType.ServiceBased &&
                contractorServices.Any(x =>
                    !x.IsDeleted &&
                    x.Type == PODContractorServiceType.ServiceBased &&
                    x.OperationInfoService != null &&
                    x.OperationInfoService.Id ==
                        request.OperationInfoService.Id))
            {
                return Result.Failure<ProjectOperationDetailContractorService>(
                    ContractorServiceErrors.ServiceIsExsist);
            }

            var entity =
                new ProjectOperationDetailContractorService(
                    request.ProjectOperationDetail,
                    request.OperationInfoService,
                    request.ProjectServiceDetail,
                    request.ContractorId,
                    request.Volume,
                    request.TimeSpant,
                    request.IsActive,
                    request.Type);

            return await _repository.Create(entity, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return Result.Failure<ProjectOperationDetailContractorService>(
                SharedErrors.UnknownError);
        }
    }
}