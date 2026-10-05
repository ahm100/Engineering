namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.CreateContractorService;

public class CreateContractorServiceValidator : AbstractValidator<CreateContractorServiceRequest>
{
    public CreateContractorServiceValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(ContractorServiceErrors.ProjectOperationDetailIdIsEmpty);
        RuleFor(oo => oo.ServiceInfoId).NotNull().WithError(ContractorServiceErrors.ServiceIdIsEmpty);
        RuleFor(oo => oo.Volume).GreaterThan(0).WithError(ContractorServiceErrors.VolumeMustGreaterThan);
        RuleFor(x => x.Type).IsNullableEnum("Type");
    }
}
