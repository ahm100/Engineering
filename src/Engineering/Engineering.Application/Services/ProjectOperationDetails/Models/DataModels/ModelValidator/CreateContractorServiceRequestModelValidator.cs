using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.ModelValidator;

public class CreateContractorServiceRequestModelValidator : AbstractValidator<CreateContractorServiceRequestModel>
{
    public CreateContractorServiceRequestModelValidator()
    {
        RuleFor(c => c.ServiceInfoId).NotNull().WithError(ContractorServicesErrors.ServiceInfoIdIsEmpty);
        RuleFor(c => c.Volume).GreaterThanOrEqualTo(0).WithError(ContractorServiceErrors.VolumeMustGreaterThan);
    }
}
