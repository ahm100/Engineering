namespace Engineering.Application.Services.Contractors.Commands.ContractorServices.CreateContractorService;

public class CreateContractorServiceCommandValidator : AbstractValidator<CreateContractorServiceCommand>
{
    public CreateContractorServiceCommandValidator()
    {
        RuleFor(c => c.ContractorId).NotNull().WithError(ContractorServicesErrors.ContractorIdIsEmpty);
        RuleFor(c => c.ServiceInfoId).NotNull().WithError(ContractorServicesErrors.ServiceInfoIdIsEmpty);
    }
}