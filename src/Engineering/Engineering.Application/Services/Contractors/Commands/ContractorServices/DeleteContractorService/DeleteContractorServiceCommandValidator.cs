namespace Engineering.Application.Services.Contractors.Commands.ContractorServices.DeleteContractorService;

public class DeleteContractorServiceCommandValidator : AbstractValidator<DeleteContractorServiceCommand>
{
    public DeleteContractorServiceCommandValidator()
    {
        RuleFor(c => c.Id).NotNull().WithError(ContractorServicesErrors.IdIsEmpty);
    }
}
