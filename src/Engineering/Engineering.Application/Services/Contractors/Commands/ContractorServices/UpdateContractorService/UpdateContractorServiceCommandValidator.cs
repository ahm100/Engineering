namespace Engineering.Application.Services.Contractors.Commands.ContractorServices.UpdateContractorService;

public class UpdateContractorServiceCommandValidator : AbstractValidator<UpdateContractorServiceCommand>
{
    public UpdateContractorServiceCommandValidator()
    {
        RuleFor(c => c.Id).NotNull().WithError(ContractorServicesErrors.InValidId);
        RuleFor(c => c.ContractorId).NotNull().WithError(ContractorServicesErrors.ContractorIdIsEmpty);
        RuleFor(c => c.ServiceInfoId).NotNull().WithError(ContractorServicesErrors.ServiceInfoIdIsEmpty);
    }
}