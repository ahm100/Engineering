
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.DisableContractorService;

public class DisableContractorServiceCommandValidator : AbstractValidator<DisableContractorServiceCommand>
{
    public DisableContractorServiceCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ContractorServiceErrors.IdIsEmpty);
    }
}