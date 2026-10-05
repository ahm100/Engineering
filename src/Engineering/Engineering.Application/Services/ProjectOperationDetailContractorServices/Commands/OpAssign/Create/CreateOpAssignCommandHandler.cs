using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.OpAssign.Create;

public class CreateOpAssignCommandHandler
    : ICommandHandler<
        CreateOpAssignCommand,
        ProjectOperationDetailContractorService>
{
    private readonly ILogger<CreateOpAssignCommandHandler> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public CreateOpAssignCommandHandler(
        ILogger<CreateOpAssignCommandHandler> logger,
        IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailContractorService?>> Handle(
    CreateOpAssignCommand request,
    CT ct)
    {
        try
        {
            var projectOperationDetail = request.ProjectOperationDetail;

            // --------------------------------------------------
            // ServiceBased + OperationBased forbidden
            // --------------------------------------------------
            var hasServiceBasedService =
                projectOperationDetail
                    .ProjectOperationDetailContractorServices
                    .Any(x =>
                        !x.IsDeleted &&
                        x.Type == PODContractorServiceType.ServiceBased);

            if (hasServiceBasedService)
                return Result.Failure<ProjectOperationDetailContractorService>(
                    ContractorServiceErrors.MixedContractorServiceTypeNotAllowed);

            var assignmentExists =
                projectOperationDetail
                    .ProjectOperationDetailContractorServices
                    .Any(x =>
                        !x.IsDeleted &&
                        x.Type == PODContractorServiceType.OperationBased &&
                        x.ContractorId == request.ContractorId);

            if (assignmentExists)
                return Result.Failure<ProjectOperationDetailContractorService>(
                    ContractorServiceErrors.OperationBasedAssignmentIsExist);

            var deductionVolume =
                projectOperationDetail.ProjectOperationDetailDeductions
                    .Where(x => !x.IsDeleted)
                    .Sum(x => x.FinalAmount);

            var assignedVolume =
                projectOperationDetail.ProjectOperationDetailContractorServices
                    .Where(x =>
                        !x.IsDeleted &&
                        x.Type == PODContractorServiceType.OperationBased)
                    .Sum(x => x.Volume);

            var assignableVolume =
                projectOperationDetail.FinalAmount - deductionVolume;

            var remainingVolume =
                assignableVolume -
                assignedVolume -
                request.ContractAllocatedConstructionQuantity;

            if (remainingVolume <= 0)
                return Result.Failure<ProjectOperationDetailContractorService>(
                    ContractorServiceErrors.OperationBasedRemainingVolumeNotAvailable);

            var volume = request.Volume ?? remainingVolume;

            if (volume <= 0)
                return Result.Failure<ProjectOperationDetailContractorService>(
                    ContractorServiceErrors.VolumeMustGreaterThan);

            if (volume > remainingVolume)
                return Result.Failure<ProjectOperationDetailContractorService>(
                    ContractorServiceErrors.OperationBasedVolumeGreaterThanRemaining);

            var entity = new ProjectOperationDetailContractorService(
                projectOperationDetail: projectOperationDetail,
                serviceInfo: null,
                projectServiceDetail: null,
                contractorId: request.ContractorId,
                volume: volume,
                timeSpant: 0,
                isActive: true,
                type: PODContractorServiceType.OperationBased);

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
