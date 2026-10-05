
namespace Engineering.Application.Services.DetailContractorServices.Commands.InactiveDetailContractorService;

public class InactiveDetailContractorServiceCommandValidator : AbstractValidator<InactiveDetailContractorServiceCommand>
{
    public InactiveDetailContractorServiceCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ContractorServiceErrors.IdIsEmpty);
    }
}
