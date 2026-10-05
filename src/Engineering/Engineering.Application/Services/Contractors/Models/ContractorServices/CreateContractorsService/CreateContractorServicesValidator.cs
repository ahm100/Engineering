namespace Engineering.Application.Services.Contractors.Models.ContractorServices.CreateContractorsService;

public class CreateContractorServicesValidator : AbstractValidator<CreateContractorServicesRequest>
{
    public CreateContractorServicesValidator()
    {
        RuleFor(c => c.ContractorId).NotNull().WithError(ContractorServicesErrors.ContractorIdIsEmpty);
        RuleFor(c => c.ServiceInfoIds).NotEmpty().WithError(ContractorServicesErrors.ServiceInfoIdIsEmpty);
    }
}