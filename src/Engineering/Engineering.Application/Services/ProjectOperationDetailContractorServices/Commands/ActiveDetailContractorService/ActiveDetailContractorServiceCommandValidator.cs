namespace Engineering.Application.Services.DetailContractorServices.Commands.ActiveDetailContractorService;

public class ActiveDetailContractorServiceCommandValidator : AbstractValidator<ActiveDetailContractorServiceCommand>
{
    public ActiveDetailContractorServiceCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ContractorServiceErrors.IdIsEmpty);
    }
}
