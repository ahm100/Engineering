using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.ModelValidator
{
    public class UpdateContractorServiceRequestModelValidator : AbstractValidator<UpdateContractorServiceRequestModel>
    {
        public UpdateContractorServiceRequestModelValidator()
        {
            When(oo => oo.Id != null, () =>
            {
                RuleFor(oo => oo.Id).NotNull().WithError(ContractorServiceErrors.IdIsEmpty);
            });
            RuleFor(c => c.ServiceInfoId).NotNull().WithError(ContractorServicesErrors.ServiceInfoIdIsEmpty);
            RuleFor(c => c.Volume).GreaterThanOrEqualTo(0).WithError(ContractorServiceErrors.VolumeMustGreaterThan);

        }
    }
}
