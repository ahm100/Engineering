using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.OpAssign.Update;

public class UpdateOpAssignCommandHandler
    : ICommandHandler<
        UpdateOpAssignCommand,
        ProjectOperationDetailContractorService>
{
    private readonly ILogger<UpdateOpAssignCommandHandler> _logger;

    public UpdateOpAssignCommandHandler(
        ILogger<UpdateOpAssignCommandHandler> logger)
    {
        _logger = logger;
    }

    public Task<Result<ProjectOperationDetailContractorService?>> Handle(
        UpdateOpAssignCommand request,
        CT ct)
    {
        try
        {
            var entity = request.Entity;

            if (entity.Type != PODContractorServiceType.OperationBased ||
                entity.IsDeleted)
            {
                return Task.FromResult(
                    Result.Failure<ProjectOperationDetailContractorService>(
                        ContractorServiceErrors.OperationBasedAssignmentNotFound));
            }

            // اگر وارد قرارداد شده باشد قابل ویرایش نیست
            var hasContract =
                entity.ContractorContractDetailServices
                    .Any(x => !x.IsDeleted);

            if (hasContract)
            {
                return Task.FromResult(
                    Result.Failure<ProjectOperationDetailContractorService>(
                        ContractorServiceErrors.OperationBasedAssignmentHasContract));
            }

            if (request.Volume <= 0)
            {
                return Task.FromResult(
                    Result.Failure<ProjectOperationDetailContractorService>(
                        ContractorServiceErrors.VolumeMustGreaterThan));
            }

            var pod = entity.ProjectOperationDetail;

            // اگر Contractor تغییر کرده، روی همین POD برای Contractor جدید
            // نباید OperationBased دیگری وجود داشته باشد.
            var duplicateContractor =
                pod.ProjectOperationDetailContractorServices.Any(x =>
                    x.Id != entity.Id &&
                    !x.IsDeleted &&
                    x.Type == PODContractorServiceType.OperationBased &&
                    x.ContractorId == request.ContractorId);

            if (duplicateContractor)
            {
                return Task.FromResult(
                    Result.Failure<ProjectOperationDetailContractorService>(
                        ContractorServiceErrors.OperationBasedDuplicateContractor));
            }

            var deductionVolume =
                pod.ProjectOperationDetailDeductions
                    .Where(x => !x.IsDeleted)
                    .Sum(x => x.FinalAmount);

            var assignableVolume =
                pod.FinalAmount - deductionVolume;

            // مجموع Assignmentهای دیگر، به جز خود رکورد جاری
            var otherAssignedVolume =
                pod.ProjectOperationDetailContractorServices
                    .Where(x =>
                        x.Id != entity.Id &&
                        !x.IsDeleted &&
                        x.Type == PODContractorServiceType.OperationBased)
                    .Sum(x => x.Volume);

            var maxAllowedVolume =
                assignableVolume -
                otherAssignedVolume -
                request.ContractAllocatedConstructionQuantity;

            if (request.Volume > maxAllowedVolume)
            {
                return Task.FromResult(
                    Result.Failure<ProjectOperationDetailContractorService>(
                        ContractorServiceErrors.OperationBasedVolumeGreaterThanRemaining));
            }

            entity.SetContractorId(request.ContractorId);
            entity.SetVolume(request.Volume);

            return Task.FromResult(
                Result.Success<ProjectOperationDetailContractorService>(entity));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return Task.FromResult(
                Result.Failure<ProjectOperationDetailContractorService>(
                    SharedErrors.UnknownError));
        }
    }
}
