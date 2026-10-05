
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.UpdateContractorService;

public class UpdateContractorServiceValidator : AbstractValidator<UpdateContractorServiceRequest>
{
    public UpdateContractorServiceValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().GreaterThanOrEqualTo(1).WithError(ContractorServiceErrors.ProjectOperationDetailIdIsEmpty);
        RuleFor(oo => oo.ContractorServiceRequests).NotEmpty().WithError(ContractorServiceErrors.ContractorServiceRequestsIsEmpty);
    }
}
